using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class OnAxeThrowCatch : UnityEvent<bool>
{

}
public class BoomeraxeGrip : MonoBehaviour
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
    Camera playerCamera = null;

    [BoxGroup("Requirement")]
    [SerializeField]
    [Required]
    GameObject boomeraxeObject = null;

    [BoxGroup("Requirement")]
    [SerializeField]
    [Required]
    IMovement holderMovement = null;


    [BoxGroup("Requirement")]
    [SerializeField]
    [Required]
    GameObject holderPivot = null;

    [BoxGroup("Requirement")]
    [SerializeField]
    [Required]
    BoomeraxeGravityScaleAdjustor adjustor = null;

    [BoxGroup("Requirement")]
    [SerializeField]
    [Required]
    Boomeraxe boomeraxeFlying = null;

    [BoxGroup("Optional")]
    [SerializeField]
    Shake shake = null;

    [BoxGroup("Optional")]
    [SerializeField]
    OnAxeThrowCatch throwCatchEvent = new OnAxeThrowCatch();

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

    bool axeIsShaking = false;
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
            if (Input.GetMouseButtonDown(0) && boomeraxeFlying.IsStuck() == true)
            {
                if (shake != null)
                {
                    LogHelper.GetInstance().Log(("ACTIVATE!!").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
                    shake.InduceTrauma(() => boomeraxeFlying.ActivateAbility());
                }
                if (holderMovement.IsTouchingGround() == false)
                {
                    adjustor.SetGravityScale(datas.timeScaleOnAxeRecall);
                }
            }
            if (OutOfCameraView() && axeIsReturning == false)
            {
                LogHelper.GetInstance().Log("Boomeraxe".Bolden().Colorize("#83ecd7") + " has exit the Camera Bounds, Return in " + datas.timeTilAxeReturnAfterExitCameraView.ToString().Bolden(), true);
                StopCoroutine(HoldAxeAfter(datas.timeTilAxeReturnAfterExitCameraView));
                StartCoroutine(HoldAxeAfter(datas.timeTilAxeReturnAfterExitCameraView));
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
        boomeraxeFlying.Fly(mousPos);
        axeCatchable = false;
        adjustor.SetGravityScaleFor(datas.timeScaleAfterThrow, datas.lulPeriodAfterAirborneThrow);
        throwCatchEvent.Invoke(true);
        StopCoroutine(TurnOnAxeCatchable(datas.timeTilAxeCatchable));
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
            LogHelper.GetInstance().Log(("Arkkkk, So heavy!").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
            LogHelper.GetInstance().Log("Catch the Axe!", true);
            throwCatchEvent.Invoke(false);
            isBeingHeld = true;
            axeCatchable = false;
            StickToHolder();
            boomeraxeFlying.Reset();
            adjustor.ResetTimeScale();
        }
    }
    public BoomeraxeGravityScaleAdjustor GetTimeAdjustor()
    {
        return adjustor;
    }
    public bool GetIsHoldingAxe()
    {
        return isBeingHeld;
    }
}
