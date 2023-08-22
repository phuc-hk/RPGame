using UnityEngine;

namespace RPGame.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon")]
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject weaponPrefab;
        [SerializeField] AnimatorOverrideController animatorOverride;

        public void Spawn(Transform weaponPosition, Animator animator)
        {
            Instantiate(weaponPrefab, weaponPosition);
            animator.runtimeAnimatorController = animatorOverride;
        }
    }
}