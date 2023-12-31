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
        //Debug.Log("Load level display");
        baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        experience = GameObject.FindWithTag("Player").GetComponent<Experience>();  
    }

    private void Start()
    {
        UpdateLevelText();
    }

    private void OnEnable()
    {
        experience.OnExperienceGain.AddListener(UpdateLevelText);
    }

    private void OnDisable()
    {
        experience.OnExperienceGain.RemoveAllListeners();
    }

    private void UpdateLevelText()
    {
        GetComponent<TextMeshProUGUI>().text = baseStats.GetLevel().ToString();
    }
}
