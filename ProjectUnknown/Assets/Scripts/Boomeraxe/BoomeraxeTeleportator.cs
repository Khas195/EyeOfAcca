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
    Movement2DPlatform holderMovement = null;


    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    int currentTeleport = 0;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    bool isCountingAirbornTime = false;

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

                // Freeze for seconds
                var holderBody = boomeraxeHolder.GetComponent<Rigidbody2D>();
                holderBody.gravityScale = 0.0f;
                holderBody.velocity = Vector2.zero;
                holderMovement.gameObject.SetActive(false);

                // Grab the axe
                grip.SetAxeCatchable(true);
                grip.HoldAxe();

                if (isCountingAirbornTime == false)
                {
                    StartCoroutine(TurnBackHolderGravity(datas.airBornTimeAfterTeleport, holderBody, holderMovement));
                }
            }
        }
    }
    IEnumerator TurnBackHolderGravity(float time, Rigidbody2D holderBody, Movement2DPlatform holderMovement)
    {
        isCountingAirbornTime = true;
        yield return new WaitForSeconds(time);
        holderBody.gravityScale = 1.0f;
        holderMovement.gameObject.SetActive(true);
        isCountingAirbornTime = false;
    }
}
