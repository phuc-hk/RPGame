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
        public float WeaponRange => weaponRange;
        public float WeaponDamage => weaponDamage;
        public GameObject Spawn(Transform rightHandPosition, Transform leftHandPosition, Animator animator)
        {         
            if(animatorOverride != null)
                animator.runtimeAnimatorController = animatorOverride;

            if (weaponPrefab != null)
            {
                Transform weaponPosition;
                if (isRightHand) weaponPosition = rightHandPosition;
                else weaponPosition = leftHandPosition;
                GameObject weapon = (GameObject)Instantiate(weaponPrefab, weaponPosition);
                return weapon;
            }
            else
                return null;
        }

    }
}