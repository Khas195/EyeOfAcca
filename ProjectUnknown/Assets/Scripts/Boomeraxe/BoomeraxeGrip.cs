﻿using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class BoomeraxeGrip : MonoBehaviour
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
    Camera playerCamera = null;


    [BoxGroup("Requirement")]
    [SerializeField]
    [Required]
    GameObject boomeraxeObject = null;

    [BoxGroup("Requirement")]
    [SerializeField]
    [Required]
    GameObject holderPivot = null;

    [BoxGroup("Requirement")]
    [SerializeField]
    [Required]
    Boomeraxe boomeraxeFlying = null;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    bool isBeingHeld = true;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    bool axeCatchable = false;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    bool axeIsReturning = false;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        HoldAxe();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (isBeingHeld == true)
        {
            StickToHolder();

            var mousPos = Input.mousePosition;
            mousPos = playerCamera.ScreenToWorldPoint(mousPos);

            if (Input.GetMouseButtonDown(0))
            {
                ThrowAxe(mousPos);
            }
        }
        else
        {
            if (OutOfCameraView() && axeIsReturning == false)
            {
                LogHelper.GetInstance().Log("Boomeraxe".Bolden().Colorize("#83ecd7") + " has exit the Camera Bounds, Return in " + datas.timeTilAxeReturnAfterExitCameraView.ToString().Bolden(), true);
                StartCoroutine(HoldAxeAfter(datas.timeTilAxeReturnAfterExitCameraView));
            }
            if (boomeraxeFlying.GetBounceCount() > datas.maxBounce)
            {
                axeCatchable = true;
                HoldAxe();
            }
        }
    }
    public void SetAxeCatchable(bool catchable)
    {
        axeCatchable = catchable;
    }

    private bool OutOfCameraView()
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(this.playerCamera);
        var boomeraxeCollider2d = boomeraxeObject.GetComponent<Collider2D>();
        if (GeometryUtility.TestPlanesAABB(planes, boomeraxeCollider2d.bounds))
        {
            return false;
        }
        return true;
    }

    private void ThrowAxe(Vector3 mousPos)
    {
        isBeingHeld = false;
        boomeraxeFlying.gameObject.SetActive(true);
        boomeraxeFlying.Fly(mousPos);
        axeCatchable = false;
        StartCoroutine(TurnOnAxeCatchable(datas.timeTilAxeCatchable));
    }
    public bool IsHoldingAxe()
    {
        return isBeingHeld;
    }

    IEnumerator TurnOnAxeCatchable(float time)
    {
        yield return new WaitForSeconds(time);
        axeCatchable = true;
    }
    IEnumerator HoldAxeAfter(float time)
    {
        axeIsReturning = true;
        yield return new WaitForSeconds(time);
        HoldAxe();
        axeIsReturning = false;
    }

    private void StickToHolder()
    {
        Vector3 pos = holderPivot.transform.position;
        pos.z = boomeraxeObject.transform.position.z;
        boomeraxeObject.transform.position = pos;
    }
    public Vector2 GetAxePosition()
    {
        return boomeraxeObject.transform.position;
    }
    public void HoldAxe()
    {
        if (axeCatchable)
        {
            LogHelper.GetInstance().Log("Catch the Axe!", true);
            boomeraxeFlying.gameObject.SetActive(false);
            isBeingHeld = true;
            axeCatchable = false;
            boomeraxeFlying.Reset();
        }
    }
}
