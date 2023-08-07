using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGame.Combat
{
    public class Fighter : MonoBehaviour
    {
        public void Attack(CombatTarget target)
        {
            Debug.Log("Attack " + target.name);
        }
    }
}