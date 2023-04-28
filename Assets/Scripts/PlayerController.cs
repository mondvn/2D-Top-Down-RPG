using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;

    private void Awake()
    {
        this.playerControls = new PlayerControls();
        this.rb = GetComponent<Rigidbody2D>();
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
        this.Move();
    }

    private void PlayerInput()
    {
        this.movement = this.playerControls.Movement.Move.ReadValue<Vector2>();
    }

    private void Move()
    {
        this.rb.MovePosition(this.rb.position + this.movement * (this.moveSpeed * Time.fixedDeltaTime));
    }
}
