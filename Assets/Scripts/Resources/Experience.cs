using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    [SerializeField] float experiencePoint;

    public void GainExperience(float experiencePoint)
    {
        this.experiencePoint += experiencePoint;
    }
}
