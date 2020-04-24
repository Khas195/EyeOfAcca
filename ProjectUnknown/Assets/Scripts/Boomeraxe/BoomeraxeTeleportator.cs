using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class BoomeraxeTeleportator : MonoBehaviour
{
    [BoxGroup("Settings")]
    [SerializeField]
    [Required]
    [InfoBox("Boomeraxe Datas - The values below are applied for ALL scripts that use this Data Object", EInfoBoxType.Warning)]
    [InfoBox("Boomeraxe Datas - The values below can ONLY be changed by clicking Save in the data object itself", EInfoBoxType.Warning)]
    [DisplayScriptableObjectProperties]
    BoomeraxeParams datas = null;


    [BoxGroup("Requirement")]
    [SerializeField]
    [Required]
    BoomeraxeGrip grip = null;

    [BoxGroup("Requirement")]
    [SerializeField]
    [Required]
    GameObject boomeraxeHolder = null;

    [BoxGroup("Requirement")]
    [SerializeField]
    [Required]
    BoomeraxeGravityScaleAdjustor adjustor = null;


    [BoxGroup("Requirement")]
    [SerializeField]
    [Required]
    Movement2DPlatform holderMovement = null;


    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    int currentTeleport = 0;

    void Start()
    {
        currentTeleport = datas.maxTeleport;
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (currentTeleport != datas.maxTeleport && holderMovement.IsTouchingGround())
        {
            LogHelper.GetInstance().Log("Player ".Bolden().Colorize(Color.green) + " has touched the ground - resetting teleport count".Bolden(), true);
            currentTeleport = datas.maxTeleport;
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (grip.IsHoldingAxe() == false && currentTeleport > 0)
            {
                currentTeleport -= 1;
                LogHelper.GetInstance().Log("Player ".Bolden().Colorize(Color.green) + " has teleported - Teleport count: ".Bolden() + currentTeleport + "/".Bolden() + datas.maxTeleport, true);

                /// teleport 
                var pos = grip.GetAxePosition();
                boomeraxeHolder.transform.position = pos;

                if (datas.lulTimeAfterTeleport > 0)
                {
                    adjustor.SetGravityScaleFor(datas.timeScaleAfterTeleport, datas.lulTimeAfterTeleport);
                }
                // Freeze for seconds


                // Grab the axe
                grip.SetAxeCatchable(true);
                grip.HoldAxe();


            }
        }
    }


}
