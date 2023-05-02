using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private Transform weaponCollider;

    private PlayerControls playerControls;
    private Animator myAnimator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;

    private GameObject slashAnim;

    private void Awake()
    {
        this.playerController = GetComponentInParent<PlayerController>();
        this.activeWeapon = GetComponentInParent<ActiveWeapon>();
        this.myAnimator = GetComponent<Animator>();
        this.playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        this.playerControls.Enable();
    }

    void Start()
    {
        this.playerControls.Combat.Attack.started += _ => this.Attack();
    }

    private void Update()
    {
        this.MouseFollowWithOffset();
    }

    private void Attack()
    {
        this.myAnimator.SetTrigger("Attack");
        this.weaponCollider.gameObject.SetActive(true);

        this.slashAnim = Instantiate(this.slashAnimPrefab, this.slashAnimSpawnPoint.position, Quaternion.identity);
        this.slashAnim.transform.parent = this.transform.parent;
    }

    public void DoneAttackingAnimEvent()
    {
        this.weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimEvent()
    {
        this.slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (this.playerController.FacingLeft)
        {
            this.slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimEvent()
    {
        this.slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (this.playerController.FacingLeft)
        {
            this.slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(this.playerController.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            this.activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            this.weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            this.activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            this.weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
