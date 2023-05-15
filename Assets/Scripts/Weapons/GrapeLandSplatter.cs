using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeLandSplatter : MonoBehaviour
{
    private SpriteFade spriteFade;
    private CapsuleCollider2D capsuleCollider2D;

    private void Awake()
    {
        this.spriteFade = GetComponent<SpriteFade>();
        this.capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        StartCoroutine(this.spriteFade.SlowFadeRoutine());
        Invoke("DisableCollider", 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        playerHealth?.TakeDamage(1, transform);
    }

    private void DisableCollider()
    {
        this.capsuleCollider2D.enabled = false;
    }


}
