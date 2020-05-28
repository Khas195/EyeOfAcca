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


    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    Text loadingText = null;

    [BoxGroup("Settings")]
    [SerializeField]
    int dotBehindText = 3;
    [BoxGroup("Settings")]
    [SerializeField]
    float dotTime = .5f;

    [BoxGroup("Status")]
    [SerializeField]
    [ReadOnly]
    int curDot = 0;


    [BoxGroup("Status")]
    [SerializeField]
    [ReadOnly]
    float curtime = 0;

    [BoxGroup("Status")]
    [SerializeField]
    [ReadOnly]
    Action callbackAfterFade = null;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {

        LoadingText();
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

    private void LoadingText()
    {
        curtime += Time.unscaledDeltaTime;
        if (curtime > dotTime)
        {
            curDot += 1;
            if (curDot > dotBehindText)
            {
                curDot = 0;
            }
            loadingText.text = "Loading";
            for (int i = 0; i < curDot; i++)
            {
                loadingText.text += '.';
            }
            curtime = 0;
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
