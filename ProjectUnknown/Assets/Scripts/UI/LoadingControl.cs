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
    [Required]
    Text loadingText = null;
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    GameObject loadingUI = null;
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    Image loadingBG = null;
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    Camera tempCam = null;
    [BoxGroup("Settings")]
    [SerializeField]
    float fadeTime = 0.5f;
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
    bool isFading = false;
    [BoxGroup("Status")]
    [SerializeField]
    [ReadOnly]
    bool isFadingOut = false;
    [BoxGroup("Status")]
    [SerializeField]
    [ReadOnly]
    float curFadingTime = 0;
    [BoxGroup("Status")]
    [SerializeField]
    [ReadOnly]
    float endBGAlpha = 0;
    [BoxGroup("Status")]
    [SerializeField]
    [ReadOnly]
    float beginBGAlpha = 0;
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
        if (isFading)
        {
            if (curFadingTime >= fadeTime)
            {
                isFading = false;
                if (callbackAfterFade != null)
                {
                    callbackAfterFade();
                    callbackAfterFade = null;
                }
                if (isFadingOut)
                {
                    this.transform.gameObject.SetActive(false);
                    isFadingOut = false;
                }
                else
                {
                    tempCam.gameObject.SetActive(true);
                }
                return;
            }
            var bgColor = loadingBG.color;
            if (isFadingOut)
            {
                bgColor.a = 1 - Tweener.LinearTween(curFadingTime, beginBGAlpha, endBGAlpha, fadeTime);
            }
            else
            {
                bgColor.a = Tweener.LinearTween(curFadingTime, beginBGAlpha, endBGAlpha, fadeTime);
            }
            curFadingTime += Time.unscaledDeltaTime;
            loadingBG.color = bgColor;

        }
    }

    private void LoadingText()
    {
        curtime += Time.deltaTime;
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
        this.gameObject.SetActive(true);
        var bgColor = loadingBG.color;
        bgColor.a = 0;
        loadingBG.color = bgColor;
        isFading = true;
        beginBGAlpha = 0;
        curFadingTime = 0;
        endBGAlpha = 1;
        callbackAfterFade = callback;
    }

    public void FadeOut(Action callback)
    {
        tempCam.gameObject.SetActive(false);
        this.gameObject.SetActive(true);
        var bgColor = loadingBG.color;
        bgColor.a = 1;
        loadingBG.color = bgColor;
        isFading = true;
        beginBGAlpha = 0;
        curFadingTime = 0;
        endBGAlpha = 1;
        callbackAfterFade = callback;
        isFadingOut = true;
    }
}
