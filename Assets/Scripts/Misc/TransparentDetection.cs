using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparentDetection : MonoBehaviour
{

    [Range(0, 1)]
    [SerializeField] private float transparencyAmount = 0.8f;
    [SerializeField] private float fadeTime = 0.4f;

    private SpriteRenderer spriteRenderer;
    private Tilemap tilemap;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.GetComponent<PlayerController>()) return;

        if (this.spriteRenderer)
        {
            StartCoroutine(this.FadeRoutine(this.spriteRenderer, this.fadeTime, this.spriteRenderer.color.a, this.transparencyAmount));
        }
        else if (this.tilemap)
        {
            StartCoroutine(this.FadeRoutine(this.tilemap, this.fadeTime, this.tilemap.color.a, this.transparencyAmount));
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.GetComponent<PlayerController>()) return;

        if (this.spriteRenderer)
        {
            StartCoroutine(this.FadeRoutine(this.spriteRenderer, this.fadeTime, this.spriteRenderer.color.a, 1f));
        }
        else if (this.tilemap)
        {
            StartCoroutine(this.FadeRoutine(this.tilemap, this.fadeTime, this.tilemap.color.a, 1f));
        }

    }

    private IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float targetTransparency)
    {
        float elapseTime = 0f;
        while (elapseTime < fadeTime)
        {
            elapseTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapseTime / this.fadeTime);
            this.spriteRenderer.color = new Color(this.spriteRenderer.color.r, this.spriteRenderer.color.g, this.spriteRenderer.color.b, newAlpha);
            yield return null;
        }
    }

    private IEnumerator FadeRoutine(Tilemap tilemap, float fadeTime, float startValue, float targetTransparency)
    {
        float elapseTime = 0f;
        while (elapseTime < fadeTime)
        {
            elapseTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapseTime / fadeTime);
            this.tilemap.color = new Color(this.tilemap.color.r, this.tilemap.color.g, this.tilemap.color.b, newAlpha);
            yield return null;
        }
    }
}
