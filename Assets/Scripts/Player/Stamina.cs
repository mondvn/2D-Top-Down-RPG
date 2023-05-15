using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : Singleton<Stamina>
{
    [SerializeField] private Sprite fullStaminaImage, emptyStaminaImage;
    [SerializeField] private int timeBetweenStaminaRefresh = 3;

    private Transform staminaContainer;
    public int CurrentStamina { get; private set; }
    private int startingStamina = 3;
    private int maxStamina;
    const string STAMINA_CONTAINER_TEXT = "Stamina Cointainer";

    protected override void Awake()
    {
        base.Awake();
        this.maxStamina = startingStamina;
        this.CurrentStamina = startingStamina;
    }

    private void Start()
    {
        this.staminaContainer = GameObject.Find(STAMINA_CONTAINER_TEXT).transform;
    }

    public void UseStamina()
    {
        this.CurrentStamina--;
        this.UpdateStamiaImages();
    }

    public void RefreshStamina()
    {
        if (this.CurrentStamina < this.maxStamina)
        {
            this.CurrentStamina++;
        }
        this.UpdateStamiaImages();
    }

    private IEnumerator RefreshStaminaRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(this.timeBetweenStaminaRefresh);
            this.RefreshStamina();
            Debug.Log("CurrentStamina: " + CurrentStamina);
            if (this.CurrentStamina >= this.maxStamina) break;
        }
    }

    private void UpdateStamiaImages()
    {
        for (int i = 0; i < this.maxStamina; i++)
        {
            if (i <= this.CurrentStamina - 1)
            {
                this.staminaContainer.GetChild(i).GetComponent<Image>().sprite = this.fullStaminaImage;
            }
            else
            {
                this.staminaContainer.GetChild(i).GetComponent<Image>().sprite = this.emptyStaminaImage;
            }
        }
        if (this.CurrentStamina < this.maxStamina)
        {
            StopAllCoroutines();
            StartCoroutine(RefreshStaminaRoutine());
        }
    }
}
