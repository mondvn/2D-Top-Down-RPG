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

        if (!transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>())
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        GameObject weaponToSpawn = transform.GetChild(activeSlotIndexNum).
        GetComponentInChildren<InventorySlot>().GetWeaponInfo().weaponPrefab;

        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);

        newWeapon.transform.parent = ActiveWeapon.Instance.transform;
        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }
}
