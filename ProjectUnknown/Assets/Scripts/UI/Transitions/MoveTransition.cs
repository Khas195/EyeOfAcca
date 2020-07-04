
using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class MoveTransition : TransitionCurve
{
    [SerializeField]
    [BoxGroup("Settings")]
    bool useUnscaleTime = false;

    [SerializeField]
    [BoxGroup("Settings")]
    [Required]
    Transform target = null;

    [SerializeField]
    [BoxGroup("Settings")]
    [Required]
    Transform begin = null;

    [SerializeField]
    [BoxGroup("Settings")]
    [Required]
    Transform end = null;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        var curValue = this.GetCurrentValue();
        SetValue(curValue);
    }
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
    private void SetValue(float curValue)
    {
        var dir = end.position - begin.position;
        dir.Normalize();
        var distance = Vector2.Distance(end.position, begin.position);
        var distanceFromBegin = curValue * distance;
        target.position = begin.position + dir * distanceFromBegin;
    }
}
