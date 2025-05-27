using Unity.VisualScripting;
using UnityEngine;

public class Ally : Characterbase
{
    private void Update()
    {
        if (isDead || target == null) return;
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance <= attackRange)
        {
            rb.linearVelocity = Vector2.zero;
            if (anim != null) anim.SetBool("isRunning", false);
            if (CanAttack()) Attack();
        }
        else
        {
            Movetowards(target);
        }
    }
    private void Movetowards(Transform target)
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
        if (anim != null) anim.SetBool("isRunning", true);
    }
    protected override void Attack()
    {
        lastAttackTime = Time.time;
        if (anim != null) anim.SetTrigger("Attack");
        if (target.TryGetComponent<Characterbase>(out var ally)) ally.TakeDamage(attackPower);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ally")) target = other.transform;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == target)
        {
            target = null;
            if (anim != null) anim.SetBool("IsRunning", false);
        }
    }
}
