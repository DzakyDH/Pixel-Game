using UnityEngine;
using UnityEngine.Rendering;

public class Characterbase : MonoBehaviour
{
    public float health = 100f;
    public float attackPower = 10f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1.5f;
    public float moveSpeed = 2f;
    public HealtBarUI healthBar;
    public float maxHealth = 100f;
    private float currentHealth;

    protected float lastAttackTime;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected Transform target;
    protected bool isDead = false;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.SetHealth(currentHealth, maxHealth);
    }
    public virtual void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
            healthBar.SetHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
            Die();
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
        if (anim != null)
        {
            anim.SetTrigger("Attack");
        }
        if (target != null)
        {
            if (target.TryGetComponent<Characterbase>(out Characterbase enemy))
            {
                enemy.TakeDamage(attackPower);
            }
            if (enemy != null)
            {
                enemy.TakeDamage(attackPower);
            }
        }
        lastAttackTime = Time.time;
    }
    protected bool CanAttack()
    {
        return Time.time - lastAttackTime >= this.attackCooldown;
    }
    protected void FaceTarget(Vector3 targetPosition)
    {
        Vector3 scale = transform.localScale;
        if (targetPosition.x < transform.position.x)
            scale.x = -Mathf.Abs(scale.x); // Menghadap kiri
        else
            scale.x = Mathf.Abs(scale.x); // Menghadap kanan
        transform.localScale = scale;
    }
}