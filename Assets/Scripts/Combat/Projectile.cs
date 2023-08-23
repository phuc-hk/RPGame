using RPGame.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Transform target = null;
    float speed = 10;
    float damage = 0;
    void Update()
    {
        if (target == null) return;
        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    public void SetTarget(Transform shootTarget, float shootDamage)
    {
        target = shootTarget;
        damage = shootDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("Hit target");
            other.GetComponent<Heath>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
