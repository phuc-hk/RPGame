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
    Fighter fighter;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fighter = GetComponent<Fighter>();
    }
    void Update()
    { 
        isInRange = chaseDistance > Vector3.Distance(player.transform.position, gameObject.transform.position);
        if (isInRange && fighter.CanAttack())
        {   
            if(!hadAttack)
            {
                print(gameObject.name + "Found player");               
                fighter.Attack(player);
                hadAttack = true;
            }                        
        }
        else
        {
            hadAttack = false;
            GetComponent<Fighter>().Cancel();
        }
    }
}
