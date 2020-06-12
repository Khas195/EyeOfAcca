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
    Texture2D transInTexture = null;

    [BoxGroup("Requirements")]
    [SerializeField]
    Texture2D transOutTexture = null;

    [BoxGroup("Requirements")]
    [SerializeField]
    public TransitionCurve transCurve = null;


    [BoxGroup("Status")]
    [SerializeField]
    [ReadOnly]
    float curtime = 0;

    [BoxGroup("Status")]
    [SerializeField]
    [ReadOnly]
    Action callbackAfterFade = null;
    void Update()
    {
        this.screenTransitionMat.SetFloat("_CutOff", transCurve.GetCurrentValue());
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
        this.screenTransitionMat.SetTexture("_TransitionTexture", transInTexture);
        this.gameObject.SetActive(true);
        callbackAfterFade = callback;
        transCurve.TransitionIn();
    }

    public void FadeOut(Action callback)
    {
        this.screenTransitionMat.SetTexture("_TransitionTexture", transOutTexture);
        callbackAfterFade = callback;
        transCurve.TransitionOut();
    }
}
