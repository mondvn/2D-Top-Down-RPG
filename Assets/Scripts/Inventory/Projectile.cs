using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleOnHitPrefabVFX;

    private WeaponInfo weaponInfo;
    private Vector3 startPosition;

    private void Start()
    {
        this.startPosition = transform.position;
    }

    private void Update()
    {
        this.moveProjectile();
        this.DetectFireDistance();
    }

    public void UpdateWeaponInfo(WeaponInfo weaponInfo)
    {
        this.weaponInfo = weaponInfo;
    }

    private void moveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        Indestructiable indestructiable = other.gameObject.GetComponent<Indestructiable>();

        if (!other.isTrigger && (enemyHealth || indestructiable))
        {
            enemyHealth?.TakeDamage(this.weaponInfo.weaponDamage);
            Instantiate(this.particleOnHitPrefabVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, this.startPosition) > this.weaponInfo.weaponRange)
            Destroy(gameObject);
    }
}
