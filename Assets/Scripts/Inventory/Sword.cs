using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private float swordAttackCD = 0.5f;

    private Animator myAnimator;
    private Transform weaponCollider;
    private GameObject slashAnim;

    private void Awake()
    {
        this.myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        this.weaponCollider = PlayerController.Instance.GetWeaponCollider();
        this.slashAnimSpawnPoint = GameObject.Find("SlimAnimSpawnPoint").transform;
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

        if (PlayerController.Instance.FacingLeft)
        {
            this.slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimEvent()
    {
        this.slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            this.slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            this.weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            this.weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
