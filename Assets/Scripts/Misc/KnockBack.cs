using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool gettingKnockedBack = false;
    [SerializeField] private float knockBackTime = 0.2f;
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
