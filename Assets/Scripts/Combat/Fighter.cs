using RPGame.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGame.Combat
{
    public class Fighter : MonoBehaviour
    {
        Transform target;
        [SerializeField] float weaponRange;
        bool isInrange;
        private void Update()
        {          
            if (target != null)
            {
                isInrange = Vector3.Distance(transform.position, target.position) < weaponRange;
                if(!isInrange)
                {
                    GetComponent<Mover>().MoveTo(target.position);                   
                }    
                else
                {
                    GetComponent<Mover>().Stop();
                    target = null;
                }
            }          
        }
        public void Attack(CombatTarget combatTarget)
        {
            Debug.Log("Attack " + combatTarget.name);
            target = combatTarget.transform;
        }
    }
}