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
    [BoxGroup("Settings")]
    [SerializeField]
    protected bool beginIn = true;


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

    Action transitionInDoneCallback = null;
    Action transitionOutDoneCallback = null;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    virtual protected void Awake()
    {
        transInLastKeyTime = transInCurve.keys[transInCurve.length - 1].time;
        transOutLastKeyTime = transOutCurve.keys[transOutCurve.length - 1].time;
        if (beginIn)
        {
            curTime = transInLastKeyTime;
        }
        else
        {
            curTime = transOutLastKeyTime;
        }
    }
    virtual protected void Start()
    {

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

    public virtual void TransitionIn(Action transitionDoneCallback = null)
    {
        this.TransitionIn(0, transitionDoneCallback);
    }

    public virtual void TransitionIn(float atTime, Action transitionDoneCallback = null)
    {
        curTime = atTime;
        curCurve = transInCurve;
        this.transitionInDoneCallback = transitionDoneCallback;
    }
    public virtual void TransitionOut()
    {
        curTime = 0;
        curCurve = transOutCurve;
    }

    public bool IsCurrentTimeInGraph()
    {
        if (curCurve.keys.Length <= 0) return false;

        if (curCurve == transInCurve)
        {
            if (curTime > transInLastKeyTime)
            {
                if (transitionInDoneCallback != null)
                {
                    this.transitionInDoneCallback();
                    this.transitionInDoneCallback = null;
                }
            }
            return curTime <= transInLastKeyTime && curTime >= 0;

        }
        else if (curCurve == transOutCurve)
        {
            if (curTime > transOutLastKeyTime)
            {
                if (transitionOutDoneCallback != null)
                {
                    this.transitionOutDoneCallback();
                    this.transitionOutDoneCallback = null;
                }
            }
            return (curTime <= transOutLastKeyTime && curTime >= 0);
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
