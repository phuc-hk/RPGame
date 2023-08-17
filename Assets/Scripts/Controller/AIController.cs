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
    [SerializeField] float patrolSpeedFraction;

    bool hadAttack = false;
    float suspicionTime = 5f;
    float timeFromLastSeenPlayer = Mathf.Infinity;
    float distanceTollerance = 1f;
    int currentWayPointIndex = 0;
    float timeFromArrivedWayPoint = Mathf.Infinity;
    float dwellingTime = 3f;

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
        //HandleAttack();
        if (IsInDetectRange() && fighter.CanAttack())
        {
             AttackBehavior();
        }
        else if (timeFromLastSeenPlayer < suspicionTime)
        {
            SuspicionBehavior();
        }
        else
        {
            PatrolBehavior();
        }

        UpdateTimer();
    }

    private void UpdateTimer()
    {
        timeFromArrivedWayPoint += Time.deltaTime;
        timeFromLastSeenPlayer += Time.deltaTime;
    }

    //void HandleAttack()
    //{
    //    if (IsInDetectRange() && fighter.CanAttack())
    //    {
    //        if (!hadAttack)
    //        {
    //            AttackBehavior();
    //        }
    //    }
    //    else if (timeFromLastSeenPlayer < suspicionTime)
    //    {
    //        SuspicionBehavior();
    //    }
    //    else
    //    {
    //        PatrolBehavior();
    //    }
    //}

    private void AttackBehavior()
    {
        if(!hadAttack)
        {
            timeFromLastSeenPlayer = 0;
            fighter.Attack(player);
            hadAttack = true;
        }    
    }

    private void SuspicionBehavior()
    {
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
                timeFromArrivedWayPoint = 0;
                CycleWayPoint();
            }
            nextPosition = GetCurrentWayPoint();
        }

        if(timeFromArrivedWayPoint > dwellingTime)
            mover.MoveTo(nextPosition, patrolSpeedFraction);
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
        //if (player.GetComponent<Heath>().IsDie()) return false;
        float distanceOfEnemyAndPlayer = Vector3.Distance(transform.position, player.transform.position);
        return  distanceOfEnemyAndPlayer < detectRange;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}
