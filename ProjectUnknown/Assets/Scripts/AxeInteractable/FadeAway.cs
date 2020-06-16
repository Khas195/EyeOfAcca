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
    List<SpriteRenderer> renders = new List<SpriteRenderer>();

    [SerializeField]
    bool doOnce = true;

    void FixedUpdate()
    {
        if (fadeCurve.IsCurrentTimeInGraph())
        {
            fadeCurve.AdvanceTime(Time.fixedDeltaTime);
            for (int i = 0; i < renders.Count; i++)
            {
                var render = renders[i];
                var color = render.color;
                color.a = fadeCurve.GetCurrentValue();
                render.color = color;
            }
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
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (doOnce && other.tag == "Player")
        {
            fadeCurve.TransitionIn();
            doOnce = false;
        }
    }
}
