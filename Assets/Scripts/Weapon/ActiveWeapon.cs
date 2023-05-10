using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    [SerializeField] private MonoBehaviour currentActiveWeapon;

    private PlayerControls playerControls;
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
    }

    private void Update()
    {
        this.Attack();
    }

    public void ToggleIsAttacking(bool value)
    {
        this.isAttacking = value;
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

        this.isAttacking = true;
        (this.currentActiveWeapon as IWeapon).Attack();
    }

}
