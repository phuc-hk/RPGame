using RPGame.Core;
using RPGame.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGame.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange;
        [SerializeField] float timeBetweenAttack = 3.0f;

        float timeSinceLastAttack = 0;
        Transform target;
        bool isInrange;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            

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
                    AttackBehavior();
                }
            }          
        }

        private void AttackBehavior()
        {
            if (timeSinceLastAttack > timeBetweenAttack)
            {
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;
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

        void Hit()
        {

        }
    }
}