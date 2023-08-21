using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RPGame.Saving;

namespace RPGame.Combat 
{
    public class Heath : MonoBehaviour, ISaveable
    {
        [SerializeField] float heath;
        public UnityEvent OnDeath;

        public void TakeDamage(float damage)
        {
            heath = Mathf.Max(heath - damage, 0);
            if (heath == 0)
            {
                GetComponent<Animator>().SetTrigger("death");
                GetComponent<BoxCollider>().enabled = false;
                OnDeath?.Invoke();
            }
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
    }
}


