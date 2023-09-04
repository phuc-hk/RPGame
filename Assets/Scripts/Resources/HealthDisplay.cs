using RPGame.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    Heath heath;
    void Awake()
    {
        heath = GameObject.FindWithTag("Player").GetComponent<Heath>();        
    }

    void Start()
    {
        UpadateHealthText();
    }
    private void OnEnable()
    {
        heath.OnHealthChange.AddListener(UpadateHealthText);
    }

    private void OnDisable()
    {
        heath.OnHealthChange.RemoveAllListeners();
    }

    private void UpadateHealthText()
    {
        //GetComponent<TextMeshProUGUI>().text = heath.GetHealthPercentage().ToString("0.0") + "%";
        GetComponent<TextMeshProUGUI>().text = String.Format("{0:0}/{1:0}", heath.GetCurrentHealth(), heath.GetMaxHealth());
    }
}
