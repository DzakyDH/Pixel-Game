using UnityEngine;

public class Characterbase : MonoBehaviour
{
    public float health = 100f;
    public float attackPower = 10f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1.5f;
    public float moveSpeed = 2f;

    protected float lastAttackTime;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected Transform target;
    protected bool isDead = false;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    public virtual void TakeDamage(float damage)
    {
        if (isDead) return;
        health -= damage;
        if (health <= 0) Die();
    }
    protected virtual void Die()
    {
        isDead = true;
        if (anim != null)
        {
            anim.SetTrigger("Die");
        }
        rb.linearVelocity = Vector2.zero;
        Destroy(gameObject, 2f);
    }
    protected virtual void Attack()
    {

    }
    protected bool CanAttack()
    {
        return Time.time - lastAttackTime >= this.attackCooldown;
    }
}