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

        void Start()
        {
            
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;

            if (target.GetComponent<Heath>().IsDie()) return;

            if (!IsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehavior();
            }

        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        private void AttackBehavior()
        {
            transform.LookAt(target);
            if (timeSinceLastAttack > timeBetweenAttack)
            {
                //This will trigger Hit() event
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;              
            }       
        }

        //Hit event of Attack Animation
        void Hit()
        {
            Heath targetHeath = target.GetComponent<Heath>();
            targetHeath.TakeDamage(5);
            targetHeath.OnDeath.AddListener(Cancel);
        }
        public void Attack(GameObject combatTarget)
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