using RPGame.Combat;
using RPGame.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPGame.Controller
{
    public class PlayerController : MonoBehaviour
    {
        void Update()
        {
            InteractWithMovement();
            InteractWithCombat();
        }

        private void InteractWithCombat()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
                foreach (RaycastHit hit in hits)
                {
                    CombatTarget combatTarget = hit.transform.GetComponent<CombatTarget>();
                    if (combatTarget == null) continue;
                    GetComponent<Fighter>().Attack(combatTarget);
                }
            }
        }

        private void InteractWithMovement()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            RaycastHit hitInfo;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hitInfo);
            if (hasHit)
            {
                GetComponent<Mover>().MoveTo(hitInfo.point);
            }
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}