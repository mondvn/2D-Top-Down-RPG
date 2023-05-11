using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicLaser : MonoBehaviour
{
    [SerializeField] private float laserGrowTime = .22f;

    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider2D;
    private float laserRange;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        this.LaserFaceMouse();
    }

    public void UpdateLaserRange(float laserRange)
    {
        this.laserRange = laserRange;
        StartCoroutine(IncreaseLaserLengthRoutine());
    }

    private IEnumerator IncreaseLaserLengthRoutine()
    {
        float timePassed = 0f;
        while (this.spriteRenderer.size.x < this.laserRange)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / this.laserGrowTime;

            this.spriteRenderer.size = new Vector2(Mathf.Lerp(1f, this.laserRange, linearT), 1f);

            this.capsuleCollider2D.size = new Vector2(Mathf.Lerp(1f, this.laserRange, linearT),
             this.capsuleCollider2D.size.y);

            this.capsuleCollider2D.offset = new Vector2(Mathf.Lerp(1f, this.laserRange / 2, linearT),
             this.capsuleCollider2D.offset.y);

            yield return null;
        }

        StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutine());
    }

    private void LaserFaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = transform.position - mousePosition;
        transform.right = -direction;
    }
}
