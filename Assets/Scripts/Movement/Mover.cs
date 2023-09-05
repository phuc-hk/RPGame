using RPGame.Combat;
using RPGame.Core;
using RPGame.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPGame.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float maxSpeed;
        NavMeshAgent navMeshAgent;
        Heath heath;

        private void Awake()
        {
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();
            heath = GetComponent<Heath>();
        }

        private void Update()
        {
            this.enabled = !heath.IsDie();
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

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        public object CaptureState()
        {
           return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            //GetComponent<NavMeshAgent>().enabled = false;
            transform.position = position.ToVector();
            //GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}