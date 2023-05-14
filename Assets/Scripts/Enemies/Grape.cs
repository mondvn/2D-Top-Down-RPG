using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grape : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject grapeProjectilePrefab;

    private Animator myAnimator;
    private SpriteRenderer spriteRenderer;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack");

    private void Awake()
    {
        this.myAnimator = GetComponent<Animator>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Attack()
    {
        myAnimator.SetTrigger(ATTACK_HASH);
        // if(transform.position.x - PlayerController.Instance.transform.position.x < 0)
        // {
        //     spriteRenderer.flipX = false;  
        // } else {
        //     spriteRenderer.flipX = true;
        // }

        this.spriteRenderer.flipX = (transform.position.x - PlayerController.Instance.transform.position.x < 0)
         ? false : true;
    }

    public void SpawnProjectileAnimEvent()
    {
        Instantiate(this.grapeProjectilePrefab, transform.position, Quaternion.identity);
    }
}
