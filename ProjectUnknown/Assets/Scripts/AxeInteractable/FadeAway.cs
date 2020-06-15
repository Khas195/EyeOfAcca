using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class FadeAway : AxeInteractable
{
    [SerializeField]
    [Required]
    TransitionCurve fadeCurve = null;

    [SerializeField]
    [Required]
    SpriteRenderer render = null;

    [SerializeField]
    bool doOnce = true;

    void FixedUpdate()
    {
        if (fadeCurve.IsCurrentTimeInGraph())
        {
            fadeCurve.AdvanceTime(Time.fixedDeltaTime);
            var color = render.color;
            color.a = fadeCurve.GetCurrentValue();
            render.color = color;
        }
    }
    public override void OnAxeHit(Boomeraxe axe)
    {
        base.OnAxeHit(axe);
        if (doOnce)
        {
            fadeCurve.TransitionIn();
            doOnce = false;
        }
    }
}
