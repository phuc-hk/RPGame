using UnityEngine;

namespace RPGame.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon")]
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject weaponPrefab;
        [SerializeField] AnimatorOverrideController animatorOverride;
        [SerializeField] float weaponRange;
        [SerializeField] float weaponDamage;
        [SerializeField] bool isRightHand;
        [SerializeField] Projectile projectile;
        public float WeaponRange => weaponRange;
        public float WeaponDamage => weaponDamage;
        public GameObject Spawn(Transform rightHandPosition, Transform leftHandPosition, Animator animator)
        {         
            if(animatorOverride != null)
                animator.runtimeAnimatorController = animatorOverride;

            if (weaponPrefab != null)
            {
                Transform weaponPosition = GetHandTransform(rightHandPosition, leftHandPosition);
                GameObject weapon = (GameObject)Instantiate(weaponPrefab, weaponPosition);
                return weapon;
            }
            else
                return null;
        }

        public void LaunchProjectile(Transform rightHandPosition, Transform leftHandPosition, Transform target)
        {
            if (target == null) return;
            Projectile newProjectile = Instantiate(projectile, GetHandTransform(rightHandPosition, leftHandPosition).position, Quaternion.identity);
            newProjectile.SetTarget(target, weaponDamage);
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
    }
}