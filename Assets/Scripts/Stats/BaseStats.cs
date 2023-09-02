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

        private void Start()
        {
            currentLevel = GetLevel();
            Experience experience = GetComponent<Experience>();
            experience.OnExperienceGain.AddListener(UpdateLevel);
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
            return progression.GetStat(stat, characterClass, GetLevel()); ;
        }

        public int GetLevel()
        {
            float currentXP = GetComponent<Experience>().GetExperience();
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

