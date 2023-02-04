using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerCard : MonoBehaviour
{
    [Header("References")] public PowerData powerData;
    public Image icon;
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] TMP_Text titleText;
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClicked);
    }

    public void SetPower(PowerData power)
    {
        powerData = power;
        icon.sprite = power.icon;
        descriptionText.text = power.description;
        titleText.text = power.displayName;
    }

    private void OnValidate()
    {
        if (powerData != null)
        {
            SetPower(powerData);
        }
    }
    
    void OnClicked()
    {
        if (powerData != null)
        {
            print("Clicked Power");
            UpgradeScreen.instance.Close();
        }
    }
}