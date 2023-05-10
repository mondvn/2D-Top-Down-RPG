using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    [SerializeField] private MonoBehaviour currentActiveWeapon;
    public MonoBehaviour CurrentActiveWeapon => currentActiveWeapon;

    private PlayerControls playerControls;
    private float timeBetweenAttacks;
    private bool attackButtonDown, isAttacking = false;


    protected override void Awake()
    {
        base.Awake();
        this.playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        this.playerControls.Enable();
    }

    private void Start()
    {
        this.playerControls.Combat.Attack.started += _ => this.StartAttacking();
        this.playerControls.Combat.Attack.canceled += _ => this.StopAttacking();

        this.AttackCooldown();
    }

    private void Update()
    {
        this.Attack();
    }

    public void NewWeapon(MonoBehaviour newWeapon)
    {
        this.currentActiveWeapon = newWeapon;
        this.AttackCooldown();
        this.timeBetweenAttacks = (this.currentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
    }

    public void WeaponNull()
    {
        this.currentActiveWeapon = null;
    }

    private void AttackCooldown()
    {
        this.isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksRoutine());
    }

    private IEnumerator TimeBetweenAttacksRoutine()
    {
        yield return new WaitForSeconds(this.timeBetweenAttacks);
        this.isAttacking = false;
    }

    private void StartAttacking()
    {
        this.attackButtonDown = true;
    }

    private void StopAttacking()
    {
        this.attackButtonDown = false;
    }

    private void Attack()
    {
        if (!this.attackButtonDown) return;
        if (this.isAttacking) return;

        this.AttackCooldown();
        (this.currentActiveWeapon as IWeapon).Attack();
    }

}
