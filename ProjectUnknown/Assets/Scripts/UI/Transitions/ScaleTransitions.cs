using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class ScaleTransitions : TransitionCurve
{

    [SerializeField]
    [BoxGroup("Settings")]
    bool useUnscaleTime = false;


    [SerializeField]
    [BoxGroup("Settings")]
    bool scaleX, scaleY, scaleZ = true;

    [SerializeField]
    [BoxGroup("Settings")]
    List<Transform> scaleTargets = new List<Transform>();
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

    private void SetValue(float curValue)
    {
        for (int i = 0; i < scaleTargets.Count; i++)
        {
            var currentScale = scaleTargets[i].localScale;
            currentScale.x = scaleX ? curValue : currentScale.x;
            currentScale.y = scaleY ? curValue : currentScale.y;
            currentScale.z = scaleZ ? curValue : currentScale.z;
            scaleTargets[i].localScale = currentScale;

        }
    }
}
