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
            [SerializeField] CharacterClass characterClass;
            [SerializeField] float[] heath;

            public CharacterClass CharacterClass => characterClass;
            public float GetHeath(int level)
            {
                return heath[level - 1];
            }
        }

        public float GetHealth(CharacterClass characterClass, int level)
        {
            foreach (ProgressionCharacterClass progressionCharacter in progressionCharacters)
            {
                if (progressionCharacter.CharacterClass == characterClass)
                {
                    return (progressionCharacter.GetHeath(level));
                }
            }
            return 0;
        }
    }


}
