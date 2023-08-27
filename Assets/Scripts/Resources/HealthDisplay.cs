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
        heath.OnHealthChange.AddListener(UpadateHealthText);
        UpadateHealthText();
    }

    private void UpadateHealthText()
    {
        GetComponent<TextMeshProUGUI>().text = heath.GetHealthPercentage().ToString("0.0") + "%";
    }
}
