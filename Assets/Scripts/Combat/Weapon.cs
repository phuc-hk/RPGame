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
        public float WeaponRange => weaponRange;
        public float WeaponDamage => weaponDamage;
        public void Spawn(Transform weaponPosition, Animator animator)
        {
            if (weaponPrefab != null)
                Instantiate(weaponPrefab, weaponPosition);
            if(animatorOverride != null)
                animator.runtimeAnimatorController = animatorOverride;
        }

    }
}