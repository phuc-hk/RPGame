using RPGame.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGame.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] Weapon weapon;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Pickup(other.GetComponent<Fighter>());
                //Destroy(gameObject);
                StartCoroutine(HideForSeconds(5));
            }
        }

        private void Pickup(Fighter fighter)
        {
            fighter.UnequipWeapon();
            fighter.EquipWeapon(weapon);
        }

        IEnumerator HideForSeconds(float hideTime)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(hideTime);
            ShowPickup(true);
        }

        private void ShowPickup(bool isShow)
        {
            GetComponent<Collider>().enabled = isShow;
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(isShow);
            }
        }

        public bool HandleRaycast(PlayerController controller)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Pickup(controller.GetComponent<Fighter>());
            }
            return true;
        }
    }

}
