using RPGame.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] float detectRange;
    bool hadAttack = false;
    GameObject player;
    Fighter fighter;
    Heath heath;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fighter = GetComponent<Fighter>();
        heath = GetComponent<Heath>();
    }
    void Update()
    {
        if (heath.IsDie()) return;
        HandleAttack();
    }

    void HandleAttack()
    {
        if (IsInDetectRange() && fighter.CanAttack())
        {
            if (!hadAttack)
            {             
                fighter.Attack(player);
                hadAttack = true;
            }
        }
        else
        {
            hadAttack = false;
            GetComponent<Fighter>().Cancel();
        }
    }

    bool IsInDetectRange()
    {
        float distanceOfEnemyAndPlayer = Vector3.Distance(transform.position, player.transform.position);
        return  distanceOfEnemyAndPlayer < detectRange;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}
