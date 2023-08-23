using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGame.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Fighter fighter = other.GetComponent<Fighter>();
                fighter.UnequipWeapon();
                fighter.EquipWeapon(weapon);
                Destroy(gameObject);
            }
        }
    }

}
