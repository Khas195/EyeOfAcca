using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScaleTransition : TransitionCurve
{
    [SerializeField]
    Transform scaleTarget = null;
    [SerializeField]
    float highlightScale = 1.3f;
    [SerializeField]
    float normalScale = 1;
    [SerializeField]
    float pressScale = 1.2f;

    float beginScale = 0;
    float targetScale = 0;

    protected override void Start()
    {
        base.Start();
        beginScale = normalScale;
        targetScale = normalScale;
        SetScale(normalScale);
    }
    void Update()
    {
        if (this.IsCurrentTimeInGraph())
        {
            this.AdvanceTime(Time.deltaTime);
            var value = this.GetCurrentValue();
            SetScale(beginScale + (targetScale - beginScale) * value);
        }

    }
    public void ScaleIn()
    {
        beginScale = 0;
        targetScale = normalScale;
        base.TransitionIn();
    }
    public void ScaleOut()
    {
        beginScale = 1;
        targetScale = 0;
        base.TransitionIn();
    }

    private void SetScale(float newScaleValue)
    {
        var newScale = scaleTarget.localScale;
        newScale.x = newScale.y = newScale.z = newScaleValue;
        scaleTarget.localScale = newScale;
    }

    public void UnHighlight()
    {
        beginScale = this.transform.localScale.x;
        targetScale = normalScale;
        this.TransitionIn();
    }
    public void Highlight()
    {
        beginScale = this.transform.localScale.x;
        targetScale = highlightScale;
        this.TransitionIn();
    }
    public void OnPress()
    {
        beginScale = this.transform.localScale.x;
        targetScale = pressScale;
        this.TransitionIn();
    }
    public void OnRelease()
    {
        beginScale = this.transform.localScale.x;
        targetScale = normalScale;
        this.TransitionIn();
    }
}
