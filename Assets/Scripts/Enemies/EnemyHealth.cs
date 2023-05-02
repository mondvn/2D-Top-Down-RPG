using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int staringHealth = 3;
    [SerializeField] private int currentHealth = 1;

    private void Start()
    {
        this.currentHealth = this.staringHealth;
    }

    public void TakeDamage(int damage)
    {
        this.currentHealth -= damage;
        this.DetectDeath();
    }

    private void DetectDeath()
    {
        if (this.currentHealth <= 0) Destroy(gameObject);
    }
}
