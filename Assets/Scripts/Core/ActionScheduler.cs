using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGame.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        MonoBehaviour currentAction;
        public void StartAction(MonoBehaviour action)
        {
            if (currentAction == action) return;
            if (currentAction != null)
                Debug.Log("Cancel " + currentAction);
            currentAction = action;
        }
    }

}

