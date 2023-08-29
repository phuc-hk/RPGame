using RPGame.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour, ISaveable
{
    [SerializeField] float experiencePoint;
    public float ExperiencePoint => experiencePoint;

    public void GainExperience(float experiencePoint)
    {
        this.experiencePoint += experiencePoint;
    }

    public object CaptureState()
    {
        return experiencePoint;
    }

    public void RestoreState(object state)
    {
        experiencePoint = (float)state;
    }
}
