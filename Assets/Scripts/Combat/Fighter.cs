using RPGame.Core;
using RPGame.Movement;
using RPGame.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGame.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float timeBetweenAttack = 3.0f;      
        [SerializeField] float chasingSpeedFraction;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform;
        [SerializeField] Weapon defaultWeapon;
        
        Weapon currentWeapon;
        GameObject equiptWeapon;

        float timeSinceLastAttack = 0;
        Transform target;
        Heath targetHeath;

        void Start()
        {
            if(currentWeapon == null)
            {
                EquipWeapon(defaultWeapon);
            }          
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            equiptWeapon = weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        public void UnequipWeapon()
        {
            Destroy(equiptWeapon);
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
            return Vector3.Distance(transform.position, target.position) < currentWeapon.WeaponRange;
        }

        private void AttackBehavior()
        {
            transform.LookAt(target);
            if (timeSinceLastAttack > timeBetweenAttack)
            {
                //This will trigger event of Attack animation
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;              
            }       
        }

        //Hit event of Attack Animation
        void Hit()
        {
            targetHeath = target.GetComponent<Heath>();
            targetHeath.TakeDamage(currentWeapon.WeaponDamage);
            //targetHeath.OnDeath.AddListener(Cancel);
        }

        //Shoot event of Attack Animation
        void Shoot()
        {
            currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
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

        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            currentWeapon = (Weapon)Resources.Load(weaponName);
            EquipWeapon(currentWeapon);
        }
    }
}