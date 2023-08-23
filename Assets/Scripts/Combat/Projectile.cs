using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Transform target = null;
    float speed = 10;
    void Update()
    {
        if (target == null) return;
        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    public void SetTarget(Transform shootTarget)
    {
        target = shootTarget; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit target");
            Destroy(gameObject);
        }
    }
}
