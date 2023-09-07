using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponAttack : MonoBehaviour
{
    public UnityEvent OnAttack;
    //[SerializeField] AudioSource audioSource;
    //[SerializeField] AudioClip attackSound;
    
    public void Attack()
    {
        OnAttack.Invoke();
        //if(!audioSource.isPlaying)
        //{
        //    audioSource.PlayOneShot(attackSound);
        //}           
    }
}
