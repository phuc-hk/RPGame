using RPGame.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] float chaseDistance;
    bool isInRange = false;
    bool hadAttack = false;
    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    { 
        isInRange = chaseDistance > Vector3.Distance(player.transform.position, gameObject.transform.position);
        if (isInRange)
        {   
            if(!hadAttack)
            {
                print(gameObject.name + "Found player");
                hadAttack = true;
                GetComponent<Fighter>().Attack(player);              
            }                        
        }
        else
        {
            hadAttack = false;
            GetComponent<Fighter>().Cancel();
        }
    }
}
