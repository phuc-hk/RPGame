using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPGame.Combat 
{
    public class Heath : MonoBehaviour
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
    }
}


