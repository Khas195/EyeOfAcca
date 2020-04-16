using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField]
    [ReadOnly]
    State currentState = null;
    [SerializeField]
    [ReadOnly]
    List<State> availableStates = new List<State>();


    void Start()
    {
        currentState = null;
        FindAvailableStates();

    }
    [Button("Find Available States")]
    private void FindAvailableStates()
    {
        LogHelper.GetInstance().Log(this + " clearing available states");
        availableStates.Clear();

        LogHelper.GetInstance().Log(this + " finding available states in children");
        var states = this.GetComponentsInChildren<State>();

        if (states.Length <= 0)
        {
            LogHelper.GetInstance().LogWarning(this + " has no available states to operate");
            return;
        }

        LogHelper.GetInstance().Log(this + " adding available states in children");
        availableStates.AddRange(states);

    }
    public void RequestState(Enum requestedStateEnum)
    {
        var requestedState = LookUpAvailableState(requestedStateEnum);
        if (requestedState == null)
        {
            LogHelper.GetInstance().LogWarning(this + " trying to request a NULL state");
            return;
        }
        if (currentState == null)
        {
            CompleteTransition(ref currentState, requestedState);
        }
        else
        {
            if (currentState.CanTransitionTo(requestedState.GetEnum()))
            {
                CompleteTransition(ref currentState, requestedState);
            }
            else
            {
                LogHelper.GetInstance().LogWarning(this + " trying to make an unsupported transition to " + requestedState);
                return;
            }
        }
    }

    public State LookUpAvailableState(Enum requestedStateEnum)
    {
        LogHelper.GetInstance().Log(this + " looking up available states for " + requestedStateEnum);
        State result = null;
        for (int i = 0; i < availableStates.Count; i++)
        {
            var curEnum = availableStates[i].GetEnum();
            if (curEnum.Equals(requestedStateEnum))
            {
                result = availableStates[i];
                break;
            }
        }
        if (result)
        {
            LogHelper.GetInstance().Log(this + " found a match for " + requestedStateEnum);
        }
        else
        {

            LogHelper.GetInstance().LogWarning(this + " CANNOT found a match for " + requestedStateEnum);
        }
        return result;
    }

    private void CompleteTransition(ref State currentState, State requestedState)
    {
        if (currentState)
        {
            LogHelper.GetInstance().Log(this + " call State exit for " + currentState);
            currentState.OnStateExit();
        }
        if (requestedState)
        {
            LogHelper.GetInstance().Log(this + " call State enter for " + requestedState);
            requestedState.OnStateEnter();
        }
        currentState = requestedState;
        LogHelper.GetInstance().Log(this + " completed transition from " + currentState + " to " + requestedState);
    }
    public State GetCurrentState()
    {
        return currentState;
    }
}
