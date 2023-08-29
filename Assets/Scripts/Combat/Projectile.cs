using RPGame.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject hitEffect;
    [SerializeField] bool isHoming;
    [SerializeField] float maxLifeTime = 10;
    Heath target = null;
    float speed = 20;
    float damage = 0;
    Vector3 offsetPosition = new Vector3(0, 1, 0);
    bool firstAim = true;
    GameObject instigator;

    private void Start()
    {
        
    }
    void Update()
    {
        if (target == null) return;

        if (firstAim)
        {
            transform.LookAt(target.transform.position + offsetPosition);
            firstAim = false;
        }

        if (isHoming)
        {
            transform.LookAt(target.transform.position + offsetPosition);
        }
       
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    public void SetTarget(GameObject instigator, Transform shootTarget, float shootDamage)
    {
        target = shootTarget.GetComponent<Heath>();
        damage = shootDamage;
        this.instigator = instigator;
        Destroy(gameObject, maxLifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (hitEffect != null)
            {
                Instantiate(hitEffect, target.transform.position + offsetPosition, Quaternion.identity);
            }
            other.GetComponent<Heath>().TakeDamage(instigator, damage);
            Destroy(gameObject);
        }
    }
}
