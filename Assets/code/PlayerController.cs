using UnityEngine;

public class Player : Characterbase
{
    private bool moveLeft;
    private bool moveRight;


    private void Update()
    {
        if (isDead) return;
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 velocity = Vector2.zero;

        if (moveLeft)
        {
            velocity = Vector2.left * moveSpeed;
            FaceDirection(Vector2.left);
        }
        else if (moveRight)
        {
            velocity = Vector2.right * moveSpeed;
            FaceDirection(Vector2.right);
        }

        rb.linearVelocity = velocity;

        if (anim != null)
            anim.SetBool("IsRunning", velocity != Vector2.zero);
    }

    public void StartMoveLeft() => moveLeft = true;
    public void StopMoveLeft() => moveLeft = false;

    public void StartMoveRight() => moveRight = true;
    public void StopMoveRight() => moveRight = false;

    public void PressAttack()
    {
        if (CanAttack())
        {
            rb.linearVelocity = Vector2.zero;
            Attack();
        }
    }

    protected override void Attack()
    {
        lastAttackTime = Time.time;

        if (anim != null)
            anim.SetTrigger("Attack");

        // Arah serangan berdasarkan arah hadap (scale.x)
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, attackRange, LayerMask.GetMask("Enemy"));
        if (hit.collider != null && hit.collider.TryGetComponent<Characterbase>(out var enemy))
        {
            enemy.TakeDamage(attackPower);
        }
    }

    private void FaceDirection(Vector2 direction)
    {
        Vector3 scale = transform.localScale;
        if (direction.x < 0)
            scale.x = -Mathf.Abs(scale.x); // Menghadap kiri
        else if (direction.x > 0)
            scale.x = Mathf.Abs(scale.x);  // Menghadap kanan

        transform.localScale = scale;
    }
}