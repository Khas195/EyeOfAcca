using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class LoadingControl : MonoBehaviour
{
    [BoxGroup("Requirements")]
    [SerializeField]
    Material screenTransitionMat = null;

    [BoxGroup("Requirements")]
    [SerializeField]
    public TransitionCurve transCurve = null;

    [BoxGroup("Status")]
    [SerializeField]
    [ReadOnly]

    Action callbackAfterFade = null;

    [BoxGroup("Status")]
    [SerializeField]
    [ReadOnly]
    float currentValue = 0;
    void Update()

    {
        this.screenTransitionMat.SetFloat("Vector1_B610FB5D", transCurve.GetCurrentValue());
        transCurve.AdvanceTime(Time.unscaledDeltaTime);

        if (transCurve.IsCurrentTimeInGraph() == false)
        {
            if (callbackAfterFade != null)
            {
                callbackAfterFade();
                callbackAfterFade = null;
                if (transCurve.IsTransIn() == false)
                {
                    this.gameObject.SetActive(false);
                }
            }
        }

    }

    public void FadeIn(Action callback)
    {
        this.gameObject.SetActive(true);
        callbackAfterFade = callback;
        transCurve.TransitionIn();
    }

    public void FadeOut(Action callback)
    {
        callbackAfterFade = callback;
        transCurve.TransitionOut();
    }
}
