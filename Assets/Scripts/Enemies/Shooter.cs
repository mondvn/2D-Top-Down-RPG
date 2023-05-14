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
    [SerializeField] private bool stagger;
    [Tooltip("Stagger has to be enabled for oscillate to work perperly")]
    [SerializeField] private bool oscillate;

    private bool isShooting = false;

    private void OnValidate()
    {
        if (this.oscillate) this.stagger = true;
        if (!this.oscillate) this.stagger = false;
        if (this.bulletMoveSpeed <= 0) this.bulletMoveSpeed = 0.1f;
        if (this.burstCount < 1) this.burstCount = 1;
        if (this.projectilesPerBurst < 1) this.projectilesPerBurst = 1;
        if (this.angleSpread == 0) this.projectilesPerBurst = 1;
        if (this.startingDistance < 0.1f) this.startingDistance = 0.1f;
        if (this.timeBetweenBursts < 0.1f) this.timeBetweenBursts = 0.1f;
        if (this.resetTime < 0.1f) this.resetTime = 0.1f;
    }

    public void Attack()
    {
        if (this.isShooting) return;
        StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine()
    {
        this.isShooting = true;
        float timeBetweenProjectiles = 0f;

        float startAngle, currentAngle, angleStep, endAngle;
        this.TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);

        if (this.stagger) timeBetweenProjectiles = timeBetweenBursts / this.projectilesPerBurst;

        for (int i = 0; i < this.burstCount; i++)
        {
            if (!oscillate)
            {
                this.TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }
            // else
            // {
            //     currentAngle = endAngle;
            //     endAngle = startAngle;
            //     startAngle = currentAngle;
            //     angleStep *= -1;
            // }

            if (oscillate && i % 2 != 1)
            {
                this.TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }
            else if (oscillate)
            {
                currentAngle = endAngle;
                endAngle = startAngle;
                startAngle = currentAngle;
                angleStep *= -1;
            }

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

                if (stagger) yield return new WaitForSeconds(timeBetweenProjectiles);
            }

            currentAngle = startAngle;

            if (!stagger) yield return new WaitForSeconds(this.timeBetweenBursts);
        }

        yield return new WaitForSeconds(this.resetTime);
        this.isShooting = false;
    }

    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep, out float endAngle)
    {
        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        endAngle = targetAngle;
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
