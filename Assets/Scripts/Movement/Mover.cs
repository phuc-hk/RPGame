using RPGame.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPGame.Movement
{
    public class Mover : MonoBehaviour
    {
        NavMeshAgent navMeshAgent;

        private void Start()
        {
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        }

        private void Update()
        {
            UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Stop()
        {
            navMeshAgent.isStopped = true;
        }
    }
}