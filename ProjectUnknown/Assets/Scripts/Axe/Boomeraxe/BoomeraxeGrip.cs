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
    BoomeraxeParams datas = null;

    [BoxGroup("Requirement")]
    [SerializeField]
    [Required]
    Flip flip = null;


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
    [BoxGroup("Optional")]
    [SerializeField]
    GameMasterSettings settings = null;

    [BoxGroup("Optional")]
    [SerializeField]
    UnityEvent axeThrowTrigger = new UnityEvent();

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    bool isBeingHeld = false;



    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    bool axeCatchable = false;



    bool axeAbilityActivated;


    void Update()
    {
        if (isBeingHeld == true)
        {
            StickToHolder();
        }
    }
    public void SetAxeCatchable(bool catchable)
    {
        axeCatchable = catchable;
    }
    public bool ThrowAxe(Vector3 at)
    {
        if (isBeingHeld == false) return false;
        flip.CheckFacing(at.x - holderPivot.transform.position.x);

        return ThrowAxe(at, holderPivot.transform.position);
    }
    public bool ThrowAxe(Vector3 at, Vector2 originThrowPoint)
    {
        axeThrowTrigger.Invoke();

        isBeingHeld = false;
        axeCatchable = false;

        boomeraxeFlying.Fly(at, originThrowPoint);

        StopCoroutine(TurnOnAxeCatchable(datas.timeTilAxeCatchable));
        StartCoroutine(TurnOnAxeCatchable(datas.timeTilAxeCatchable));
        return true;
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
        yield return new WaitForSeconds(time);
        boomeraxeFlying.ActivateAbility();
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

    public void HoldAxe(bool force = false)
    {
        if (axeCatchable || force == true)
        {
            LogHelper.GetInstance().Log(("Arkkkk, So heavy!").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
            LogHelper.GetInstance().Log("Catch the Axe!", true);
            throwCatchEvent.Invoke(false);
            isBeingHeld = true;
            axeCatchable = false;
            this.boomeraxeFlying.Reset();
            StickToHolder();
            adjustor.ResetTimeScale();
        }
    }

    public Boomeraxe GetAxeFlying()
    {
        return boomeraxeFlying;
    }

    public BoomeraxeGravityScaleAdjustor GetTimeAdjustor()
    {
        return adjustor;
    }
    public bool GetIsHoldingAxe()
    {
        return isBeingHeld;
    }

    public bool ActivateAxeAbility()
    {
        if (boomeraxeFlying.IsStuck() == false || axeAbilityActivated == true) return false;
        if (this.settings.isNewGame)
        {
            this.settings.isNewGame = false;
            this.settings.SaveData();
        }
        axeAbilityActivated = true;
        if (shake != null)
        {
            LogHelper.GetInstance().Log(("ACTIVATE!!").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
            var soundEffect = SFXSystem.GetInstance().PlaySound(SFXResources.SFXList.AxeShaking);
            shake.InduceTrauma(() =>
            {
                boomeraxeFlying.ActivateAbility();
                axeAbilityActivated = false;
                if (soundEffect)
                {
                    soundEffect.Stop();
                }
            });
        }
        if (holderMovement.IsTouchingGround() == false)
        {
            adjustor.SetGravityScale(datas.timeScaleOnAxeRecall);
        }
        return true;
    }
}
