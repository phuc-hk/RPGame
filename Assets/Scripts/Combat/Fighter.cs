using RPGame.Core;
using RPGame.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGame.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenAttack = 3.0f;
        
        [SerializeField] float chasingSpeedFraction;
        [SerializeField] Transform handTransform = null;
        [SerializeField] Weapon weapon;

        float timeSinceLastAttack = 0;
        Transform target;
        Heath targetHeath;

        void Start()
        {
            SpawnWeapon();
        }

        private void SpawnWeapon()
        {
            if (weapon == null || handTransform == null) return;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(handTransform, animator);
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;

            if (target.GetComponent<Heath>().IsDie())
            {
                Cancel();
                return;
            }

            if (!IsInWeaponRange())
            {
                GetComponent<Mover>().MoveTo(target.position, chasingSpeedFraction);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehavior();
            }

        }

        private bool IsInWeaponRange()
        {
            return Vector3.Distance(transform.position, target.position) < weapon.WeaponRange;
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
            targetHeath = target.GetComponent<Heath>();
            targetHeath.TakeDamage(weapon.WeaponDamage);
            //targetHeath.OnDeath.AddListener(Cancel);
        }
        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;           
        }

        public bool CanAttack()
        {
            Heath heath = GetComponent<Heath>();
            return !heath.IsDie();
        }
        public void Cancel()
        {
           target = null;
        }
    }
}