using GameDevTV.Inventories;
using UnityEngine;

namespace RPGame.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Equip Weapon")]
    public class WeaponConfig : EquipableItem
    {
        [SerializeField] GameObject weaponPrefab;
        [SerializeField] AnimatorOverrideController animatorOverride;
        [SerializeField] float weaponRange;
        [SerializeField] float weaponDamage;
        [SerializeField] float percentageBonus;
        [SerializeField] bool isRightHand;
        [SerializeField] Projectile projectile;
        public float WeaponRange => weaponRange;
        public float WeaponDamage => weaponDamage;
        public float WeaponBonus => percentageBonus;
        WeaponAttack weaponAttack = null;
        GameObject weapon;

        public GameObject Spawn(Transform rightHandPosition, Transform leftHandPosition, Animator animator)
        {         
            if(animatorOverride != null)
                animator.runtimeAnimatorController = animatorOverride;

            if (weaponPrefab != null)
            {
                Transform weaponPosition = GetHandTransform(rightHandPosition, leftHandPosition);
                weapon = (GameObject)Instantiate(weaponPrefab, weaponPosition);
                return weapon;
            }
            else
                return null;
        }

        public void LaunchProjectile(Transform rightHandPosition, Transform leftHandPosition,GameObject instigator, Transform target, float damage)
        {
            if (target == null) return;
            Projectile newProjectile = Instantiate(projectile, GetHandTransform(rightHandPosition, leftHandPosition).position, Quaternion.identity);
            newProjectile.SetTarget(instigator, target, damage);
        }

        private Transform GetHandTransform(Transform rightHandPosition, Transform leftHandPosition)
        {
            Transform weaponPosition;
            if (isRightHand) weaponPosition = rightHandPosition;
            else weaponPosition = leftHandPosition;
            return weaponPosition;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void OnAttack()
        {
            if (weaponPrefab != null)
            {
                weaponAttack = weapon.GetComponent<WeaponAttack>();
                weaponAttack.Attack();
            }
        }
    }
}