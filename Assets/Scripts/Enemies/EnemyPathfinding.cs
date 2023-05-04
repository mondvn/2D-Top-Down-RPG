using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private KnockBack knockBack;

    private void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.knockBack = GetComponent<KnockBack>();
    }

    private void FixedUpdate()
    {
        this.Move();
    }

    private void Move()
    {
        if (knockBack.gettingKnockedBack) return;

        this.rb.MovePosition(this.rb.position + this.moveDir * (this.moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 targetPosition)
    {
        this.moveDir = targetPosition;
    }
}
