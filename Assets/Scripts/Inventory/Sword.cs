using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private Transform weaponCollider;
    [SerializeField] private float swordAttackCD = 0.5f;

    private Animator myAnimator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;

    private GameObject slashAnim;

    private void Awake()
    {
        this.playerController = GetComponentInParent<PlayerController>();
        this.activeWeapon = GetComponentInParent<ActiveWeapon>();
        this.myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        this.MouseFollowWithOffset();
    }
    public void Attack()
    {
        this.myAnimator.SetTrigger("Attack");
        this.weaponCollider.gameObject.SetActive(true);

        this.slashAnim = Instantiate(this.slashAnimPrefab, this.slashAnimSpawnPoint.position, Quaternion.identity);
        this.slashAnim.transform.parent = this.transform.parent;
        StartCoroutine(AttackCDRoutine());
    }

    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(this.swordAttackCD);
        ActiveWeapon.Instance.ToggleIsAttacking(false);
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
