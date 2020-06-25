using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class FadeTransition : TransitionCurve
{
    [SerializeField]
    [BoxGroup("Settings")]
    bool useUnscaleTime = false;


    [SerializeField]
    [BoxGroup("Settings")]
    List<Graphic> targetImages = new List<Graphic>();

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        var curValue = this.GetCurrentValue();
        SetValue(curValue);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsCurrentTimeInGraph())
        {
            var curValue = this.GetCurrentValue();
            SetValue(curValue);
            float deltaTime = useUnscaleTime ? Time.unscaledDeltaTime : Time.deltaTime;
            this.AdvanceTime(deltaTime);
        }
    }

    private void SetValue(float value)
    {
        for (int i = 0; i < targetImages.Count; i++)
        {
            var curColor = targetImages[i].color;
            curColor.a = value;
            targetImages[i].color = curColor;


        }
    }
}
