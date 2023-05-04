using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int staringHealth = 3;
    [SerializeField] private int currentHealth = 1;
    private KnockBack knockBack;
    private Flash flash;

    private void Awake()
    {
        this.knockBack = GetComponent<KnockBack>();
        this.flash = GetComponent<Flash>();
    }

    private void Start()
    {
        this.currentHealth = this.staringHealth;
    }

    public void TakeDamage(int damage)
    {
        this.currentHealth -= damage;
        this.knockBack.GetKnockedBack(PlayerController.Instance.transform, 15f);
        StartCoroutine(this.flash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }

    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(this.flash.GetResotreMatTime());
        this.DetectDeath();
    }

    private void DetectDeath()
    {
        if (this.currentHealth <= 0) Destroy(gameObject);
    }
}
