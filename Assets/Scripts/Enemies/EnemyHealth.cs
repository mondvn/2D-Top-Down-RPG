using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int staringHealth = 3;
    [SerializeField] private int currentHealth = 1;
    [SerializeField] private float knockBackThrust = 15f;
    [SerializeField] private GameObject deathVFXPrefab;
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
        this.knockBack.GetKnockedBack(PlayerController.Instance.transform, this.knockBackThrust);
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
        if (this.currentHealth <= 0)
        {
            Instantiate(this.deathVFXPrefab, transform.position, Quaternion.identity);
            GetComponent<PickupSpawner>().DropItems();
            Destroy(gameObject);
        }
    }
}
