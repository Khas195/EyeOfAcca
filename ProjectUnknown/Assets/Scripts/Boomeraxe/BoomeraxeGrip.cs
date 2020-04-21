using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class BoomeraxeGrip : MonoBehaviour
{

    [SerializeField]
    [Required]
    Camera playerCamera = null;
    [SerializeField]
    [Required]
    GameObject boomeraxeObject = null;
    [SerializeField]
    [Required]
    GameObject holderPivot = null;
    [SerializeField]
    [Required]
    Boomeraxe boomeraxeFlying = null;

    [SerializeField]
    [ReadOnly]
    bool isBeingHeld = true;
    [SerializeField]
    float timeTilAxeCatchable = 0.5f;

    [SerializeField]
    float timeTilAxeReturnAfterExitCameraView = 0.5f;
    [SerializeField]
    int bounceLimit = 2;
    bool axeCatchable = false;
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
            if (Input.GetMouseButtonDown(0))
            {
                axeCatchable = true;
                HoldAxe();
            }
            if (OutOfCameraView() && axeIsReturning == false)
            {
                LogHelper.GetInstance().Log("Boomeraxe".Bolden().Colorize("#83ecd7") + " has exit the Camera Bounds, Return in " + timeTilAxeReturnAfterExitCameraView.ToString().Bolden(), true);
                StartCoroutine(HoldAxeAfter(timeTilAxeReturnAfterExitCameraView));
            }
            if (boomeraxeFlying.GetBounceCount() > bounceLimit)
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
        StartCoroutine(TurnOnAxeCatchable(timeTilAxeCatchable));
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
