using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGame.Combat 
{
    public class Heath : MonoBehaviour
    {
        [SerializeField] float heath;

        public void TakeDamage(float damage)
        {
            heath = Mathf.Max(heath - damage, 0);
            print(heath);
        }
    }
}


