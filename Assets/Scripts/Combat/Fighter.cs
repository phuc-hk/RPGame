using RPGame.Core;
using RPGame.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGame.Combat
{
    public class Fighter : MonoBehaviour, IAction
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
                    GetComponent<Mover>().Cancel();
                }
            }          
        }
        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
           target = null;
        }
    }
}