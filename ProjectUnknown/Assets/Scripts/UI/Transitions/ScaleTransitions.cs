using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class ScaleTransitions : TransitionCurve
{
    [SerializeField]
    [BoxGroup("Settings")]
    bool useCurrentObject = true;

    [SerializeField]
    [BoxGroup("Settings")]
    bool useUnscaleTime = false;


    [SerializeField]
    [BoxGroup("Settings")]
    bool scaleX, scaleY, scaleZ;

    [SerializeField]
    [BoxGroup("Settings")]
    [HideIf("useCurrentObject")]
    [Required]
    Transform scaleTarget = null;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (useCurrentObject)
        {
            scaleTarget = this.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsCurrentTimeInGraph())
        {
            var curValue = this.GetCurrentValue();

            var currentScale = scaleTarget.localScale;
            currentScale.x = scaleX ? curValue : currentScale.x;
            currentScale.y = scaleY ? curValue : currentScale.y;
            currentScale.z = scaleZ ? curValue : currentScale.z;
            scaleTarget.localScale = currentScale;

            float deltaTime = useUnscaleTime ? Time.unscaledDeltaTime : Time.deltaTime;
            this.AdvanceTime(deltaTime);
        }
    }
}
