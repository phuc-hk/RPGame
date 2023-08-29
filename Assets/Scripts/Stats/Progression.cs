using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGame.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression")]
    public class Progression : ScriptableObject
    {
        [SerializeField]
        ProgressionCharacterClass[] progressionCharacters;

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public ProgressionStat[] progressionStats;

        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] level;
        }

        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            foreach (ProgressionCharacterClass progressionCharacter in progressionCharacters)
            {
                if (progressionCharacter.characterClass == characterClass)
                {
                    foreach (ProgressionStat progressionStat in progressionCharacter.progressionStats)
                    {
                        if (progressionStat.stat == stat)
                        {
                            return progressionStat.level[level - 1];
                        }
                    }
                }
            }
            return 0;
        }
    }
}
