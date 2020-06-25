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
    [Required]
    Image targetImage;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsCurrentTimeInGraph())
        {
            var curValue = this.GetCurrentValue();

            var curColor = targetImage.color;
            curColor.a = this.GetCurrentValue();
            targetImage.color = curColor;

            float deltaTime = useUnscaleTime ? Time.unscaledDeltaTime : Time.deltaTime;
            this.AdvanceTime(deltaTime);
        }
    }
}
