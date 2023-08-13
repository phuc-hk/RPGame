using RPGame.Combat;
using RPGame.Controller;
using RPGame.Core;
using RPGame.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] float detectRange;
    [SerializeField] PatrolPath patrolPath;

    bool hadAttack = false;
    float suspicionTime = 5f;
    float timeFromLastSeenPlayer = Mathf.Infinity;

    float distanceTollerance = 1f;
    int currentWayPointIndex = 0;

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
                AttackBehavior();
            }
        }
        else if (timeFromLastSeenPlayer < suspicionTime)
        {
            SuspicionBehavior();
        }
        else
        {
            PatrolBehavior();
        }
    }

    private void AttackBehavior()
    {
        timeFromLastSeenPlayer = 0;
        fighter.Attack(player);
        hadAttack = true;
    }

    private void SuspicionBehavior()
    {
        timeFromLastSeenPlayer += Time.deltaTime;
        GetComponent<ActionScheduler>().CancelCurrentAction();
    }

    private void PatrolBehavior()
    {
        hadAttack = false;
        GetComponent<Fighter>().Cancel();

        Vector3 nextPosition = guardPositon;

        if (patrolPath != null)
        {
            if (AtWayPoint())
            {
                CycleWayPoint();
            }
            nextPosition = GetCurrentWayPoint();
        }

        mover.MoveTo(nextPosition);
    }

    private Vector3 GetCurrentWayPoint()
    {
        return patrolPath.GetWayPoint(currentWayPointIndex);
    }

    private void CycleWayPoint()
    {
        currentWayPointIndex = patrolPath.GetNextIndex(currentWayPointIndex);
    }

    private bool AtWayPoint()
    {
       float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWayPoint());
       return distanceToWaypoint < distanceTollerance;
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
