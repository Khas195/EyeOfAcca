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
    float curTime = -1;
    [BoxGroup("Status")]
    [SerializeField]
    [ReadOnly]
    float transInLastKeyTime = 0.0f;
    [BoxGroup("Status")]
    [SerializeField]
    [ReadOnly]
    float transOutLastKeyTime = 0.0f;

    virtual protected void Start()
    {
        transInLastKeyTime = transInCurve.keys[transInCurve.length - 1].time;
        transOutLastKeyTime = transOutCurve.keys[transOutCurve.length - 1].time;
    }

    public float GetCurrentValue()
    {
        return curCurve.Evaluate(curTime);
    }
    public void AdvanceTime(float deltaTime)
    {
        curTime += deltaTime;
    }

    public Keyframe GetTransitionInKey(int i)
    {
        return transInCurve.keys[i];
    }

    public float GetCurrentTime()
    {
        return curTime;
    }

    public void TransitionIn()
    {
        this.TransitionIn(0);
    }
    public void TransitionIn(float atTime)
    {
        curTime = atTime;
        curCurve = transInCurve;
    }
    public void TransitionOut()
    {
        curTime = 0;
        curCurve = transOutCurve;
    }

    public bool IsCurrentTimeInGraph()
    {
        if (curCurve.keys.Length <= 0) return false;

        if (curCurve == transInCurve)
        {
            return curTime <= transInLastKeyTime && curTime >= 0;
        }
        else if (curCurve == transOutCurve)
        {

            return curTime <= transOutLastKeyTime && curTime >= 0;
        }
        else
        {
            return false;
        }

    }

    public bool IsTransIn()
    {
        return curCurve == transInCurve;
    }
}
