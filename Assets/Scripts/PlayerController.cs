using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;

    private void Awake()
    {
        this.playerControls = new PlayerControls();
        this.rb = GetComponent<Rigidbody2D>();
        this.myAnimator = GetComponent<Animator>();
        this.mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        this.playerControls.Enable();
    }

    private void Update()
    {
        this.PlayerInput();
    }

    private void FixedUpdate()
    {
        this.AdjustPlayerFacingDirection();
        this.Move();
    }

    private void PlayerInput()
    {
        this.movement = this.playerControls.Movement.Move.ReadValue<Vector2>();
        this.myAnimator.SetFloat("moveX", movement.x);
        this.myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        this.rb.MovePosition(this.rb.position + this.movement * (this.moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRenderer.flipX = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
        }

    }
}
