using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class TransitionCurve : MonoBehaviour
{

    [BoxGroup("Settings")]
    [SerializeField]
    AnimationCurve transInCurve = null;
    [BoxGroup("Settings")]
    [SerializeField]
    AnimationCurve transOutCurve = null;

    [BoxGroup("Status")]
    [SerializeField]
    [ReadOnly]
    AnimationCurve curCurve = null;

    [BoxGroup("Status")]
    [SerializeField]
    [ReadOnly]

    float curTime = 0;

    public float GetCurrentValue()
    {
        return curCurve.Evaluate(curTime);
    }
    public void AdvanceTime(float deltaTime)
    {
        curTime += deltaTime;
    }
    public float GetCurrentTime()
    {
        return curTime;
    }

    public void TransitionIn()
    {
        curTime = 0;
        curCurve = transInCurve;
    }
    public void TransitionOut()
    {
        curTime = 0;
        curCurve = transOutCurve;
    }

    public bool IsCurrentTimeInGraph()
    {
        if (curCurve.keys.Length < 1)
        {
            return false;
        }
        float lastKeyTime = curCurve.keys[curCurve.length - 1].time;

        return curTime <= lastKeyTime && curTime >= 0;
    }

    public bool IsTransIn()
    {
        return curCurve == transInCurve;
    }
}
