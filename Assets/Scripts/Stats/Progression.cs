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

        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable;

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
            BuidLookup();
            float[] levels = lookupTable[characterClass][stat];
            if (levels.Length < level)
            {
                return 0;
            }
            return levels[level-1];
        }

        public int GetLevelLength(Stat stat, CharacterClass characterClass)
        {
            BuidLookup();
            float[] levels = lookupTable[characterClass][stat];
            return levels.Length;
        }

        void BuidLookup()
        {
            if (lookupTable != null) return;

            lookupTable = new();

            foreach (ProgressionCharacterClass progressionCharacterClass in progressionCharacters)
            {
                var statLookup = new Dictionary<Stat, float[]>();
                foreach (ProgressionStat progressionStat in progressionCharacterClass.progressionStats)
                {
                    statLookup[progressionStat.stat] = progressionStat.level;
                }
                lookupTable[progressionCharacterClass.characterClass] = statLookup;
            }
        }
    }
}
