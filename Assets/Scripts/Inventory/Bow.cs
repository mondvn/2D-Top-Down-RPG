using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;

    private Animator myAnimator;

    readonly int FIRE_HASH = Animator.StringToHash("Fire");

    private void Awake()
    {
        this.myAnimator = GetComponent<Animator>();
    }

    public void Attack()
    {
        this.myAnimator.SetTrigger(FIRE_HASH);
        GameObject newArrow = Instantiate(this.arrowPrefab, this.arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
    }

    public WeaponInfo GetWeaponInfo()
    {
        return this.weaponInfo;
    }
}
