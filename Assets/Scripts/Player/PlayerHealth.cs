using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThurstAmounth = 5f;
    [SerializeField] private float damageRecoveryTime = 1f;

    private int currentHealth;
    private bool canTakeDamage = true;

    private KnockBack knockBack;
    private Flash flash;

    private void Awake()
    {
        this.flash = GetComponent<Flash>();
        this.knockBack = GetComponent<KnockBack>();
    }

    private void Start()
    {
        this.currentHealth = this.maxHealth;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (enemy && this.canTakeDamage)
        {
            this.TakeDamage(1);
            this.knockBack.GetKnockedBack(other.gameObject.transform, this.knockBackThurstAmounth);
            StartCoroutine(this.flash.FlashRoutine());
        }
    }

    private void TakeDamage(int damageAmount)
    {
        this.canTakeDamage = false;
        this.currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(this.damageRecoveryTime);
        this.canTakeDamage = true;
    }

}
