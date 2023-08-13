using RPGame.Combat;
using RPGame.Core;
using RPGame.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] float detectRange;

    bool hadAttack = false;
    float suspicionTime = 5f;
    float timeFromLastSeenPlayer = Mathf.Infinity;

    GameObject player;
    Fighter fighter;
    Mover mover;
    Heath heath;
    Vector3 guardPositon;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fighter = GetComponent<Fighter>();
        mover = GetComponent<Mover>();
        heath = GetComponent<Heath>();
        guardPositon = transform.position;
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
                timeFromLastSeenPlayer = 0;
                fighter.Attack(player);
                hadAttack = true;
            }
        }
        else if (timeFromLastSeenPlayer < suspicionTime)
        {
            timeFromLastSeenPlayer += Time.deltaTime;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
        else 
        {
            hadAttack = false;
            GetComponent<Fighter>().Cancel();
            GetComponent<Mover>().MoveTo(guardPositon);
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
