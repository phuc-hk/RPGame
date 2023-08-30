using RPGame.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceDisplay : MonoBehaviour
{
    Heath heath;
    Experience experience;
    Transform target;
    Fighter fighter;
    void Awake()
    {
        fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        fighter.OnAssignTarget.AddListener(AssignEnemyHealth);
    }

    private void AssignEnemyHealth()
    {
        target = fighter.GetTarget();
        heath = target.GetComponent<Heath>();
        heath.OnDeath.AddListener(UpadateExperienceText);
        UpadateExperienceText();
    }

    private void UpadateExperienceText()
    {
        GetComponent<TextMeshProUGUI>().text = experience.ExperiencePoint.ToString();
    }
}
