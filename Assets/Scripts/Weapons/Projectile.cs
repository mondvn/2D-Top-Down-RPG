using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleOnHitPrefabVFX;
    [SerializeField] private bool isEnemyProjectile;
    [SerializeField] private float projectileRange = 10f;

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

    public void UpdateProjectileRange(float projectileRange)
    {
        this.projectileRange = projectileRange;
    }

    public void UpdateMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    private void moveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        Indestructiable indestructiable = other.gameObject.GetComponent<Indestructiable>();
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

        if (!other.isTrigger && (enemyHealth || indestructiable || playerHealth))
        {
            if ((playerHealth && this.isEnemyProjectile) || enemyHealth && !this.isEnemyProjectile)
            {
                playerHealth?.TakeDamage(1, transform);
                Instantiate(this.particleOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else if (!other.isTrigger && indestructiable)
            {
                Instantiate(this.particleOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }

        }
    }

    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, this.startPosition) > this.projectileRange)
            Destroy(gameObject);
    }
}
