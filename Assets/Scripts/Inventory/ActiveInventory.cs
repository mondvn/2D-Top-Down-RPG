using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    private int activeSlotIndexNum = 0;

    private PlayerControls playerControls;

    private void Awake()
    {
        this.playerControls = new PlayerControls();
    }

    private void Start()
    {
        this.playerControls.Inventory.Keyboard.performed += ctx => this.ToggleActiveSlot((int)ctx.ReadValue<float>());

        this.ToggleActiveHighlight(0);
    }

    private void OnEnable()
    {
        this.playerControls.Enable();
    }

    private void ToggleActiveSlot(int numValue)
    {
        if (numValue - 1 == this.activeSlotIndexNum) return;

        this.ToggleActiveHighlight(numValue - 1);
    }

    private void ToggleActiveHighlight(int indexNum)
    {
        this.activeSlotIndexNum = indexNum;
        this.DeactiveAllHighlight();

        transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);
        this.ChangeActiveWeapon();
    }

    private void DeactiveAllHighlight()
    {
        foreach (Transform inventorySlot in transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void ChangeActiveWeapon()
    {
        if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        Transform childTransform = transform.GetChild(activeSlotIndexNum);
        InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
        WeaponInfo weaponInfo = inventorySlot.GetWeaponInfo();
        GameObject weaponToSpawn = weaponInfo?.weaponPrefab;

        if (weaponInfo == null)
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position,
         Quaternion.identity);
        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        newWeapon.transform.parent = ActiveWeapon.Instance.transform;
        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }
}
