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
    [SerializeField]
    int throwLimit = 1;
    [SerializeField]
    IMovement bodyMovement = null;

    [SerializeField]
    [ReadOnly]
    int currentThrowCount = 0;
    [SerializeField]
    [ReadOnly]
    bool axeCatchable = false;
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
        ResetThrowCount();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (currentThrowCount <= 0 && bodyMovement.IsTouchingGround())
        {
            ResetThrowCount();
        }
        if (isBeingHeld == true)
        {
            StickToHolder();

            var mousPos = Input.mousePosition;
            mousPos = playerCamera.ScreenToWorldPoint(mousPos);

            if (Input.GetMouseButtonDown(0) && currentThrowCount > 0)
            {
                ThrowAxe(mousPos);
                currentThrowCount -= 1;
                LogHelper.GetInstance().Log("Throw count decreased by 1 - Current Throw Count: ".Bolden() + currentThrowCount.ToString().Bolden(), true);
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
    public void ResetThrowCount()
    {
        currentThrowCount = throwLimit;
        LogHelper.GetInstance().Log("Reseting throw count to throw Limit : ".Bolden() + currentThrowCount.ToString().Bolden(), true);
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
