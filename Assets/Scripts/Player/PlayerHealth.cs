using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth>
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThurstAmounth = 5f;
    [SerializeField] private float damageRecoveryTime = 1f;


    private Slider healthSlider;
    private int currentHealth;
    private bool canTakeDamage = true;

    private KnockBack knockBack;
    private Flash flash;

    protected override void Awake()
    {
        base.Awake();
        this.flash = GetComponent<Flash>();
        this.knockBack = GetComponent<KnockBack>();
        this.healthSlider = GameObject.Find("Heart Slider").GetComponent<Slider>();
    }

    private void Start()
    {
        this.currentHealth = this.maxHealth;
        this.UpdateHealthSlider();
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
        this.UpdateHealthSlider();
        this.CheckIfPlayerDeath();
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(this.damageRecoveryTime);
        this.canTakeDamage = true;
    }

    public void HealPlayer()
    {
        if (this.currentHealth == this.maxHealth) return;
        this.currentHealth += 1;
        this.UpdateHealthSlider();
    }

    private void UpdateHealthSlider()
    {
        // if (this.healthSlider == null) return;

        this.healthSlider.maxValue = maxHealth;
        this.healthSlider.value = currentHealth;
    }

    private void CheckIfPlayerDeath()
    {
        if (this.currentHealth <= 0)
        {
            this.currentHealth = 0;
            Debug.Log("Player Death");
        }
    }

}
