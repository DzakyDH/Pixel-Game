using UnityEngine;

public class Enemy : Characterbase
{
    public LayerMask enemyLayer;
    public float detectionRange = 5f; // Jarak deteksi musuh
    public float stopDistance = 0.5f; // Jarak minimum ke target sebelum berhenti
    public float attackRanged = 1.5f;   // Jarak serang
    private bool facingRight = true;


    private void Update()
    {
        if (isDead) return;

        FindNearestTarget();

        if (target != null && target != transform)
        {
            float distance = Vector2.Distance(transform.position, target.position);

            if (distance > stopDistance)
            {
                MoveTowards(target);
            }
            else
            {
                StopMoving();
            }

            if (distance <= attackRange && CanAttack())
            {
                StopMoving();
                Attack();
            }
        }
        else
        {
            StopMoving();
        }
    }
   
    private void MoveTowards(Transform target)
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
        Flip(direction.x);
        if (anim != null) anim.SetBool("IsRunning", true);
    }

    private void StopMoving()
    {
        rb.linearVelocity = Vector2.zero;
        if (anim != null) anim.SetBool("IsRunning", false);
    }

    protected override void Attack()
    {
        lastAttackTime = Time.time;

        if (anim != null)
            anim.SetTrigger("Attack");

        if (target != null && target != transform)
        {
            if (target.TryGetComponent<Ally>(out var ally))
            {
                ally.TakeDamage(attackPower);
            }
            if (target.TryGetComponent<Player>(out var player))
            {
                player.TakeDamage(attackPower);
            }
        }
    }
    private void Flip(float moveX)
    {
        if ((moveX > 0 && !facingRight) || (moveX < 0 && facingRight))
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

        private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"{gameObject.name} mendeteksi {other.name}");
        if (other.CompareTag("Ally"))
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == target)
        {
            target = null;
            StopMoving();
        }
    }
    private void FindNearestTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRange, enemyLayer);

        float closestDistance = float.MaxValue;
        Transform nearest = null;

        foreach (Collider2D hit in hits)
        {
            if (hit.transform == transform) continue;

            float dist = Vector2.Distance(transform.position, hit.transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                nearest = hit.transform;
            }
        }

        target = nearest;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
