using UnityEngine;

public class Ally : Characterbase
{
    public LayerMask enemyLayer;
    public float detectionRange = 5f;  // Jarak untuk mulai mengejar musuh
    public float stopDistance = 0.5f;  // Jarak minimum sebelum berhenti
    public float attackRanged = 1.5f;   // Jarak untuk menyerang
    public GameObject healthBarPrefab;
    private HealtBarUI HealthBar;


    protected virtual void Start()
    {
        GameObject bar = Instantiate(healthBarPrefab, GameObject.Find("Canvas").transform);
        healthBar = bar.GetComponent<HealtBarUI>();
    }
    private void Update()
    {
        if (isDead) return;

        if (HealthBar != null)
        {
            HealthBar.UpdatePosition(transform.position + Vector3.up * 1.5f);
        }

        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectionRange, enemyLayer);
        if (hit != null && hit.transform != transform)
        {
            target = hit.transform;

            float distance = Vector2.Distance(transform.position, target.position);

            if (distance > stopDistance)
            {
                MoveTowards(target);
                FaceTarget(target.position);
            }
            else
            {
                StopMoving();
            }

            // Serang jika dalam jarak serangan dan cooldown selesai
            if (distance <= attackRange && CanAttack())
            {
                StopMoving();
                Attack();
            }
        }
        else
        {
            target = null;
            StopMoving();
        }
    }

    private void MoveTowards(Transform target)
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;

        if (anim != null)
            anim.SetBool("IsRunning", true);
    }

    private void StopMoving()
    {
        rb.linearVelocity = Vector2.zero;

        if (anim != null)
            anim.SetBool("IsRunning", false);
    }

    protected override void Attack()
    {
        lastAttackTime = Time.time;

        if (anim != null)
            anim.SetTrigger("Attack");

        if (target != null && target != transform)
        {
            if (target.TryGetComponent<Characterbase>(out var enemy))
            {
                enemy.TakeDamage(attackPower);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log($"{gameObject.name} mendeteksi {other.name}");
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}