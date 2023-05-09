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

        this.DeactiveAllHighlight();
        this.ToggleActiveHighlight(numValue - 1);
    }

    private void ToggleActiveHighlight(int indexNum)
    {
        this.activeSlotIndexNum = indexNum;
        transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);
    }

    private void DeactiveAllHighlight()
    {
        foreach (Transform inventorySlot in transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }
    }
}
