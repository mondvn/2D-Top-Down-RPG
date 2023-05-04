using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] private float knockBackTime = 0.2f;

    private Rigidbody2D rb;

    private bool gettingKnockedBack = false;
    public bool GettingKnockedBack => gettingKnockedBack;

    private void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockedBack(Transform damageSource, float knockBackThurst)
    {
        this.gettingKnockedBack = true;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackThurst * this.rb.mass;
        this.rb.AddForce(difference, ForceMode2D.Impulse);

        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        gettingKnockedBack = false;
    }
}
