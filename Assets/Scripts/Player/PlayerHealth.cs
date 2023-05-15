using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Singleton<PlayerHealth>
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThurstAmounth = 5f;
    [SerializeField] private float damageRecoveryTime = 1f;

    private int currentHealth;
    private bool canTakeDamage = true;

    private KnockBack knockBack;
    private Flash flash;

    protected override void Awake()
    {
        base.Awake();
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

        if (enemy)
        {
            this.TakeDamage(1, other.transform);
        }
    }

    public void TakeDamage(int damageAmount, Transform hitTransform)
    {
        if (!this.canTakeDamage) return;

        SceneShakeManager.Instance.ShakeScreen();
        this.knockBack.GetKnockedBack(hitTransform, this.knockBackThurstAmounth);
        StartCoroutine(this.flash.FlashRoutine());

        this.canTakeDamage = false;
        this.currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(this.damageRecoveryTime);
        this.canTakeDamage = true;
    }

    public void HealPlayer()
    {
        this.currentHealth += 1;
    }

}
