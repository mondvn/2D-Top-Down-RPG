using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 1f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;

    private bool facingLeft = false;
    public bool FacingLeft => facingLeft;

    private void Awake()
    {
        this.playerControls = new PlayerControls();
        this.rb = GetComponent<Rigidbody2D>();
        this.myAnimator = GetComponent<Animator>();
        this.mySpriteRender = GetComponent<SpriteRenderer>();
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

        this.myAnimator.SetFloat("moveX", this.movement.x);
        this.myAnimator.SetFloat("moveY", this.movement.y);
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
            this.mySpriteRender.flipX = true;
            this.facingLeft = true;
        }
        else
        {
            this.mySpriteRender.flipX = false;
            this.facingLeft = false;
        }
    }
}
