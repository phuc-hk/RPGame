using GameDevTV.Inventories;
using RPGame.Core;
using RPGame.Movement;
using RPGame.Saving;
using RPGame.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPGame.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable, IModifierProvider
    {
        [SerializeField] float timeBetweenAttack = 3.0f;      
        [SerializeField] float chasingSpeedFraction;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform;
        [SerializeField] WeaponConfig defaultWeapon;
        
        WeaponConfig currentWeapon;
        GameObject equiptWeapon;
        Equipment equipment;
        float timeSinceLastAttack = 0;
        Transform target;
        Heath targetHeath;

        public UnityEvent OnAssignTarget;

        void Awake()
        {
            equipment = GetComponent<Equipment>();
            if (equipment)
            {
                equipment.equipmentUpdated += UpdateWeapon;
            }
        }

        private void UpdateWeapon()
        {
            var weapon = (WeaponConfig)equipment.GetItemInSlot(EquipLocation.Weapon);
            if (weapon == null)
            {
                EquipWeapon(defaultWeapon);
            }
            else
            {
                EquipWeapon(weapon);
            }
        }

        void Start()
        {
            if(currentWeapon == null)
            {
                EquipWeapon(defaultWeapon);
            }          
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
            if (target == null) return;
            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
            targetHeath = target.GetComponent<Heath>();
            targetHeath.TakeDamage(gameObject, damage);
            currentWeapon.OnAttack();
            //targetHeath.OnDeath.AddListener(Cancel);
        }

        //Shoot event of Attack Animation
        void Shoot()
        {
            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
            currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, gameObject, target, damage);
        }

        public IEnumerable<float> GetAddictiveModifier(Stat stat)
        {
           if (stat == Stat.Damage)
            {
                yield return currentWeapon.WeaponDamage;
            }
        }

        public IEnumerable<float> GetPercentageModifier(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeapon.WeaponBonus;
            }
        }


        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
            OnAssignTarget?.Invoke();
        }

        public Transform GetTarget()
        {
            return target;
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

        public void EquipWeapon(WeaponConfig weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            equiptWeapon = weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        public void UnequipWeapon()
        {
            Destroy(equiptWeapon);
        }

        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            currentWeapon = (WeaponConfig)Resources.Load(weaponName);
            EquipWeapon(currentWeapon);
        }

        
    }
}