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


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0 || characterMovement.IsTouchingGround())
        {

            characterMovement.SetTimeScale(1.0f);
        }
    }

    public void SetGravityScaleFor(float scale, float time)
    {
        LogHelper.GetInstance().Log(("Initiate gravity scale adjustment for " + characterBody.name.Colorize(Color.green)).Bolden(), true);
        LogHelper.GetInstance().Log(("Current Settings: Gravity Scale - " + scale + ", time: " + time).Bolden(), true);
        characterMovement.SetTimeScale(scale);
        currentTime = time;
    }

}
