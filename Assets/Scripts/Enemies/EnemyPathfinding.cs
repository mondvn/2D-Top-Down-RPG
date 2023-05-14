using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private KnockBack knockBack;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.knockBack = GetComponent<KnockBack>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        this.Move();
    }

    private void Move()
    {
        if (this.knockBack.GettingKnockedBack) return;

        this.rb.MovePosition(this.rb.position + this.moveDir * (this.moveSpeed * Time.fixedDeltaTime));

        if (this.moveDir.x < 0)
        {
            this.spriteRenderer.flipX = true;
        }
        else if (this.moveDir.x > 0)
        {
            this.spriteRenderer.flipX = false;
        }
    }

    public void MoveTo(Vector2 targetPosition)
    {
        this.moveDir = targetPosition;
    }

    public void StopMoving()
    {
        this.moveDir = Vector3.zero;
    }
}
