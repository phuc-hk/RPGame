using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Legacy;
using UnityEngine;

namespace RPGame.Stats
{
    public class BaseStats : MonoBehaviour
    {
        //[Range(1, 99)]
        //[SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpEffect = null;
        int currentLevel = 0;
        Experience experience;
        private void Awake()
        {
             experience = GetComponent<Experience>();
        }

        private void OnEnable()
        {
            experience.OnExperienceGain.AddListener(UpdateLevel);
        }

        private void OnDisable()
        {
            experience.OnExperienceGain.RemoveAllListeners();
        }

        private void Start()
        {
            currentLevel = GetLevel();            
        }

        private void UpdateLevel()
        {
            int newLevel = GetLevel();
            if (newLevel > currentLevel)
            {
                currentLevel = newLevel;
                LevelUpEffect();
            }
        }

        private void LevelUpEffect()
        {
            Instantiate(levelUpEffect, transform);
        }

        public float GetStat(Stat stat)
        {
            return (GetBaseStat(stat) + GetAddictiveModifier(stat)) * (1 + GetPercentageModifier(stat) / 100) ;
        }

        

        private float GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        private float GetAddictiveModifier(Stat stat)
        {
            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAddictiveModifier(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        private float GetPercentageModifier(Stat stat)
        {
            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetPercentageModifier(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        public int GetLevel()
        {
            float currentXP = experience.GetExperience();
            int levelLength = progression.GetLevelLength(Stat.ExperienceToLevelUp, characterClass);
            for (int level = 1; level <= levelLength; level++)
            {
                float experienceToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if (experienceToLevelUp >= currentXP)
                {
                    return level;
                }
            }
            return levelLength + 1;
        }
    }

}

