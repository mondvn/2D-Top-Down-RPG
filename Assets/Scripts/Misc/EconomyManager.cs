using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EconomyManager : Singleton<EconomyManager>
{
    private TMP_Text goldText;
    private int currentGold = 0;

    protected override void Awake()
    {
        base.Awake();
        this.goldText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();

    }

    const string COIN_AMOUNT_TEXT = "Gold Amount Text";

    public void UpdateCurrentGold()
    {
        this.currentGold += 1;
        this.goldText.text = this.currentGold.ToString("D3");
    }

}
