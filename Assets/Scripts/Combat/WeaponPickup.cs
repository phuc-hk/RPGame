using System;
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
                //Destroy(gameObject);
                StartCoroutine(HideForSeconds(5));
            }
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
    }

}
