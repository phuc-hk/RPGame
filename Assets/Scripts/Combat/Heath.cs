using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RPGame.Saving;
using RPGame.Stats;

namespace RPGame.Combat 
{
    public class Heath : MonoBehaviour, ISaveable
    {
        float heath = -1;
        public UnityEvent OnDeath;
        public UnityEvent OnHealthChange;

        void Awake()
        {
            if (heath < 0)
            {
                heath = GetComponent<BaseStats>().GetStat(Stat.Health);
            }               
        }
        public void TakeDamage(GameObject instigator, float damage)
        {
            heath = Mathf.Max(heath - damage, 0);
            OnHealthChange?.Invoke();
            if (heath == 0)
            {
                GainExperience(instigator);
                Die();
            }
        }

        private void GainExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            BaseStats baseStats = GetComponent<BaseStats>();
            experience.GainExperience(baseStats.GetStat(Stat.ExperienceReward));
        }

        private void Die()
        {
            GetComponent<Animator>().SetTrigger("death");
            GetComponent<BoxCollider>().enabled = false;
            OnDeath?.Invoke();
        }

        public bool IsDie()
        {
            return heath == 0;
        }

        public object CaptureState()
        {
            return heath;
        }

        public void RestoreState(object state)
        {
            heath = (float)state;

            if (heath == 0)
            {
                GetComponent<Animator>().SetTrigger("death");
                GetComponent<BoxCollider>().enabled = false;
                OnDeath?.Invoke();
            }
        }

        public float GetCurrentHealth()
        {
            return heath;
        }

        public float GetMaxHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }
        public float GetHealthPercentage()
        {
            return 100 * (heath / GetComponent<BaseStats>().GetStat(Stat.Health));
        }
    }
}


