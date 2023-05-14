using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletMoveSpeed = 6;
    [SerializeField] private int burstCount = 5;
    [SerializeField] private int projectilesPerBurst = 6;
    [SerializeField][Range(0, 359)] private float angleSpread = 160f;
    [SerializeField] private float startingDistance = 0.1f;
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

        float startAngle, currentAngle, angleStep;
        this.TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep);

        for (int i = 0; i < this.burstCount; i++)
        {
            for (int j = 0; j < this.projectilesPerBurst; j++)
            {
                Vector2 pos = FindBulletSpawnPos(currentAngle);

                GameObject newBullet = Instantiate(this.bulletPrefab, pos, Quaternion.identity);
                newBullet.transform.right = newBullet.transform.position - transform.position;

                if (newBullet.TryGetComponent(out Projectile projectile))
                {
                    projectile.UpdateMoveSpeed(this.bulletMoveSpeed);
                }

                currentAngle += angleStep;
            }

            currentAngle = startAngle;


            yield return new WaitForSeconds(this.timeBetweenBursts);
            this.TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep);
        }

        yield return new WaitForSeconds(this.resetTime);
        this.isShooting = false;
    }

    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep)
    {
        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        float endAngle = targetAngle;
        currentAngle = targetAngle;
        float halfAngleSpread = 0f;
        angleStep = 0f;
        if (this.angleSpread != 0)
        {
            angleStep = this.angleSpread / (this.projectilesPerBurst - 1);
            halfAngleSpread = angleSpread / 2f;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }
    }

    private Vector2 FindBulletSpawnPos(float currentAngle)
    {
        float x = transform.position.x + this.startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + this.startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);
        Vector2 pos = new Vector2(x, y);
        return pos;
    }

}
