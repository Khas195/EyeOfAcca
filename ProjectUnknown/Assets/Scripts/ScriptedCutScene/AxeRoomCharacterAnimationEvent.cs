using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AxeRoomCharacterAnimationEvent : MonoBehaviour
{
    public static UnityEvent OnRaisingArmsDone = new UnityEvent();
    public static UnityEvent OnPullingArmsDownDone = new UnityEvent();

    public void RaisingArmsDone()
    {
        OnRaisingArmsDone.Invoke();
    }
    public void PullingArmsDone()
    {
        OnPullingArmsDownDone.Invoke();
    }
}
