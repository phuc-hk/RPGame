using RPGame.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthDisplay : MonoBehaviour
{
    Heath heath;
    Transform target;
    Fighter fighter;
    void Awake()
    {
        fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();              
    }

    private void OnEnable()
    {
        fighter.OnAssignTarget.AddListener(AssignEnemyHealth);
    }

    private void OnDisable()
    {
        fighter.OnAssignTarget.RemoveAllListeners();
    }

    private void AssignEnemyHealth()
    {
        target = fighter.GetTarget();
        heath = target.GetComponent<Heath>();
        heath.OnHealthChange.AddListener(UpadateHealthText);
        UpadateHealthText();
    }

    private void UpadateHealthText()
    {
        //GetComponent<TextMeshProUGUI>().text = heath.GetHealthPercentage().ToString("0.0") + "%";
        GetComponent<TextMeshProUGUI>().text = String.Format("{0:0}/{1:0}", heath.GetCurrentHealth(), heath.GetMaxHealth());
    }
}
