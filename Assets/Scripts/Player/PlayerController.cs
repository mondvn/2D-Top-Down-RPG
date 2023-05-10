using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private float dashTime = .2f;
    [SerializeField] private float dashCD = .25f;
    [SerializeField] private TrailRenderer myTrailRenderer;
    [SerializeField] private Transform weaponCollider;

    private PlayerControls playerControls;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;


    private Vector2 movement;
    private float startingMoveSpeed;
    private bool isDashing = false;
    private bool facingLeft = false;
    public bool FacingLeft => facingLeft;

    protected override void Awake()
    {
        base.Awake();

        this.playerControls = new PlayerControls();
        this.rb = GetComponent<Rigidbody2D>();
        this.myAnimator = GetComponent<Animator>();
        this.mySpriteRender = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        this.playerControls.Enable();
    }

    private void Start()
    {
        this.startingMoveSpeed = this.moveSpeed;
        this.playerControls.Combat.Dash.performed += _ => this.Dash();
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

    public Transform GetWeaponCollider()
    {
        return this.weaponCollider;
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

    private void Dash()
    {
        if (isDashing) return;

        this.isDashing = true;
        this.moveSpeed *= this.dashSpeed;
        this.myTrailRenderer.emitting = true;
        StartCoroutine(EndDashRoutine());
    }

    private IEnumerator EndDashRoutine()
    {
        yield return new WaitForSeconds(this.dashTime);
        this.moveSpeed = this.startingMoveSpeed;
        this.myTrailRenderer.emitting = false;

        yield return new WaitForSeconds(this.dashCD);
        this.isDashing = false;
    }
}
