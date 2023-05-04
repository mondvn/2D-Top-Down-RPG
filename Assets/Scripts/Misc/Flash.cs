using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMat;
    [SerializeField] private float restoreDefaultMatTime = .2f;

    private Material defaultMat;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.defaultMat = this.spriteRenderer.material;
    }

    public float GetResotreMatTime()
    {
        return this.restoreDefaultMatTime;
    }

    public IEnumerator FlashRoutine()
    {
        this.spriteRenderer.material = this.whiteFlashMat;
        yield return new WaitForSeconds(this.restoreDefaultMatTime);
        this.spriteRenderer.material = this.defaultMat;
    }
}
