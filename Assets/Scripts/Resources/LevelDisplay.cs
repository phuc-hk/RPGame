using RPGame.Combat;
using RPGame.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    Experience experience;
    BaseStats baseStats;
    void Awake()
    {
        baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        experience.OnExperienceGain.AddListener(UpdateLevelText);
        UpdateLevelText();
    }

    private void UpdateLevelText()
    {
        GetComponent<TextMeshProUGUI>().text = baseStats.GetLevel().ToString();
    }
}
