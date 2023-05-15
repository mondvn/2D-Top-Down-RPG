using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private enum PickUpType
    {
        GoldCoin,
        StaminaGlobe,
        HealthGlobe,
    }

    [SerializeField] private PickUpType pickUpType;
    [SerializeField] private float pickupDistance = 5f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float accelartionRate = 5f;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float heightY = 1.5f;
    [SerializeField] private float popDuration = 1f;

    private Vector3 moveDir;
    private Rigidbody2D rb;

    private void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(AnimCurveSpawnRoutine());
    }

    private void Update()
    {
        this.CheckDistance();
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            this.DetectPickupType();
            Destroy(gameObject);
        }
    }

    private IEnumerator AnimCurveSpawnRoutine()
    {
        Vector2 startPos = transform.position;
        float randomX = transform.position.x + Random.Range(-2f, 2f);
        float randomY = transform.position.y + Random.Range(-1f, 1f);
        Vector2 endPos = new Vector2(randomX, randomY);

        float timePassed = 0f;

        while (timePassed < this.popDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / this.popDuration;
            float heightT = this.animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, this.heightY, heightT);

            transform.position = Vector2.Lerp(startPos, endPos, linearT) + new Vector2(0f, height);
            yield return null;
        }

    }

    private void DetectPickupType()
    {
        switch (this.pickUpType)
        {
            case PickUpType.GoldCoin:
                Debug.Log("Gold Coin");
                break;
            case PickUpType.HealthGlobe:
                PlayerHealth.Instance.HealPlayer();
                Debug.Log("Health Globe");

                break;
            case PickUpType.StaminaGlobe:
                Debug.Log("Stamina Globe");
                break;

        }
    }
}
