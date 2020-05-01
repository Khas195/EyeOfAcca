using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class BoomeraxeGravityScaleAdjustor : MonoBehaviour
{
    [SerializeField]
    [Required]
    Rigidbody2D characterBody = null;
    [SerializeField]
    [Required]
    Movement2DPlatform characterMovement = null;
    [ReadOnly]
    float currentTime = 0;
    bool countingDown = false;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (countingDown)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0 || characterMovement.IsTouchingGround())
            {
                ResetTimeScale();
            }
        }
    }

    public void SetGravityScaleFor(float scale, float time)
    {
        LogHelper.GetInstance().Log(("Initiate gravity scale adjustment for " + characterBody.name.Colorize(Color.green)).Bolden(), true);
        LogHelper.GetInstance().Log(("Current Settings: Gravity Scale - " + scale + ", time: " + time).Bolden(), true);
        characterMovement.SetTimeScale(scale);
        currentTime = time;
        countingDown = true;
    }
    public void SetGravityScale(float scale)
    {
        LogHelper.GetInstance().Log(("Current Settings: Gravity Scale - " + scale).Bolden(), true);
        characterMovement.SetTimeScale(scale);
        countingDown = false;
    }
    public void ResetTimeScale()
    {
        LogHelper.GetInstance().Log(("Current Settings: Gravity Scale - " + 1).Bolden(), true);
        characterMovement.SetTimeScale(1.0f);
        countingDown = false;
    }
}
