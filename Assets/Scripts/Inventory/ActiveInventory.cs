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
        Debug.Log(transform.GetChild(activeSlotIndexNum).GetComponent<InventorySlot>().GetWeaponInfo().weaponPrefab.name);
    }
}
