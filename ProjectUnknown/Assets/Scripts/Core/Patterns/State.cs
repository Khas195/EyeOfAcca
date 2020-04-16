using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class State : MonoBehaviour
{
    [SerializeField]
    List<State> possibleTransitions = new List<State>();
    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    public virtual bool CanTransitionTo(Enum stateEnum)
    {
        LogHelper.GetInstance().Log(this + " checking transition to  " + stateEnum);
        for (int i = 0; i < possibleTransitions.Count; i++)
        {
            if (possibleTransitions[i].GetEnum().Equals(stateEnum))
            {
                LogHelper.GetInstance().Log(this + " found possible transition to " + stateEnum);
                return true;
            }
        }
        LogHelper.GetInstance().LogWarning(this + " CANNOT found possible transition to " + stateEnum);
        return false;
    }

    public abstract Enum GetEnum();
}
