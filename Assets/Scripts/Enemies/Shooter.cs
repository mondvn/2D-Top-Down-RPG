using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletMoveSpeed = 6;
    [SerializeField] private int burstCount = 5;
    [SerializeField] private float timeBetweenBursts = 0.3f;
    [SerializeField] private float resetTime = 0.5f;

    private bool isShooting = false;

    public void Attack()
    {
        if (this.isShooting) return;
        StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine()
    {
        this.isShooting = true;

        for (int i = 0; i < this.burstCount; i++)
        {
            Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
            GameObject newBullet = Instantiate(this.bulletPrefab, transform.position, Quaternion.identity);
            newBullet.transform.right = targetDirection;

            if (newBullet.TryGetComponent(out Projectile projectile))
            {
                projectile.UpdateMoveSpeed(this.bulletMoveSpeed);
            }
            yield return new WaitForSeconds(this.timeBetweenBursts);
        }

        yield return new WaitForSeconds(this.resetTime);
        this.isShooting = false;
    }

}
