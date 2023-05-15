using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private float pickupDistance = 5f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float accelartionRate = 5f;

    private Vector3 moveDir;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        this.CheckDistance();
        // this.rb.velocity = moveDir * moveSpeed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        this.rb.velocity = moveDir * moveSpeed * Time.fixedDeltaTime;
    }

    private void CheckDistance()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position;

        if (Vector3.Distance(transform.position, playerPos) < this.pickupDistance)
        {
            this.moveDir = (playerPos - transform.position).normalized;
            this.moveSpeed += this.accelartionRate;
        }
        else
        {
            this.moveDir = Vector3.zero;
            this.moveSpeed = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            Destroy(gameObject);
        }
    }
}
