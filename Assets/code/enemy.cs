using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Characterbase
{
    public LayerMask enemyLayer;
    private void Update()
    {
        if (isDead) return;

        // Cari musuh dalam jangkauan
        Collider2D hit = Physics2D.OverlapCircle(transform.position, attackRange, enemyLayer);
        if (hit != null)
        {
            target = hit.transform;

            Vector2 dir = (target.position - transform.position).normalized;
            rb.linearVelocity = dir * moveSpeed;

            if (anim != null)
            {
                MoveTowards(target);
            }

            // Serang jika sudah cukup waktu
            if (CanAttack())
            {
                rb.linearVelocity = Vector2.zero;
                Attack();
            }
        }
        else
        {
            target = null;
            rb.linearVelocity = Vector2.zero;

            if (anim != null)
            {
                anim.SetBool("IsRunning", false);
            }
        }
    }
    private void MoveTowards(Transform targer)
    {
        Vector2 direction = (target.position - targer.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
        if (anim == null) anim.SetBool("isRunning", true);
    }
    protected override void Attack()
    {
      lastAttackTime = Time.time;
        if (anim != null) anim.SetTrigger("Attack");
        if (target.TryGetComponent<Characterbase>(out var ally)) ally.TakeDamage(attackPower);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"{gameObject.name} mendeteksi {other.name}");
        if (other.CompareTag("Ally")) target = other.transform;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == target)
        {
            target = null;
            if (anim != null) anim.SetBool("isRunning", false);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
