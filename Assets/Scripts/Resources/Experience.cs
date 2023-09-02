using RPGame.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Experience : MonoBehaviour, ISaveable
{
    float experiencePoint;
    public float ExperiencePoint => experiencePoint;

    public UnityEvent OnExperienceGain;

    public void GainExperience(float experiencePoint)
    {
        this.experiencePoint += experiencePoint;
        OnExperienceGain?.Invoke();
    }

    public float GetExperience()
    {
        return experiencePoint;
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
