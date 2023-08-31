using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterEffect : MonoBehaviour
{
    [SerializeField] GameObject targetObject = null;
    void Update()
    {
        if (!GetComponent<ParticleSystem>().IsAlive())
        {
            if (targetObject != null)
            {
                Destroy(targetObject);
            }
            else
            {
                Destroy(gameObject);
            }    
            
        }
    }
}
