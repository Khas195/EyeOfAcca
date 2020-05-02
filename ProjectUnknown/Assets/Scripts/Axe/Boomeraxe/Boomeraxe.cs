﻿using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Experimental scripts for spawning fire vfxxxx
/// </summary>
[Serializable]
public class BallBounceEvent : UnityEvent<Vector3, Quaternion>
{

}
public class Boomeraxe : MonoBehaviour
{
    [BoxGroup("Settings")]
    [SerializeField]
    [Required]
    BoomeraxeParams datas = null;

    [BoxGroup("Requirement")]
    [SerializeField]
    [Required]
    Rigidbody2D body2d = null;

    [BoxGroup("Requirement")]
    [SerializeField]
    [Required]
    BoomeraxeGrip grip = null;
    [BoxGroup("Requirement")]
    [SerializeField]
    [Required]
    Transform axeHolderPos = null;



    [BoxGroup("Requirement")]
    [SerializeField]
    [Required]
    Rigidbody2D holderBody2d = null;



    [BoxGroup("Requirement")]
    [SerializeField]
    Animator animator = null;


    [BoxGroup("Requirement")]
    [SerializeField]
    AxeAbility defaultAbility = null;

    [BoxGroup("Optional")]
    [SerializeField]
    BallBounceEvent onBounce = new BallBounceEvent();



    [BoxGroup("Optional")]
    [SerializeField]
    AxeAbility activeAbility = null;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    bool flyTriggered = false;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    bool returning = false;



    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    Vector2 currentFlyDirection = Vector2.zero;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    float currentRecallTime = 0.0f;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    Vector2 stuckPos = Vector2.one;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    bool isStuck = false;
    void Start()
    {
        body2d.gravityScale = 0;
        Reset();
    }

    void FixedUpdate()
    {

        animator.SetBool("isStuck", isStuck);

        if (flyTriggered)
        {
            if (returning)
            {
                body2d.transform.position = Tweener.OutInQuartic(currentRecallTime, stuckPos, holderBody2d.transform.position, datas.recallDuration);
                currentRecallTime += Time.deltaTime;
            }
            else
            {
                Vector2 pos = body2d.transform.position;
                body2d.MovePosition(pos + currentFlyDirection * datas.flyVelocity * Time.fixedDeltaTime);
            }
        }
    }

    public bool IsStuck()
    {
        return isStuck;
    }

    public void Fly(Vector2 target)
    {
        LogHelper.GetInstance().Log("Player ".Bolden().Colorize(Color.green) + "has thrown the " + "Boomeraxe".Bolden().Colorize("#83ecd7"), true);

        body2d.gameObject.SetActive(true);
        Vector2 pos = axeHolderPos.transform.position;
        currentFlyDirection = (target - pos).normalized;
        body2d.transform.position = axeHolderPos.transform.position;
        returning = false;
        SetFlyTrigger(true);
    }

    private void SetFlyTrigger(bool triggered)
    {
        flyTriggered = triggered;
    }

    public void Reset()
    {
        body2d.gameObject.SetActive(false);
        currentFlyDirection = Vector3.zero;
        body2d.velocity = Vector2.zero;
        returning = false;
        SetFlyTrigger(false);
        isStuck = false;
        body2d.GetComponent<Collider2D>().isTrigger = false;
        onBounce.Invoke(body2d.transform.position, body2d.transform.rotation);
    }
    ContactPoint2D contactPoint;

    public void HandleCollision(Collision2D other)
    {
        if (flyTriggered == false) return;

        LogHelper.GetInstance().Log("*THUD*".Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);

        RotateBladeTowardImpactPoint(other);
        PlaceAxeAtContactPoint(other);

        body2d.GetComponent<Collider2D>().isTrigger = true;
        SetFlyTrigger(false);
        isStuck = true;
        currentFlyDirection = Vector2.zero;
        onBounce.Invoke(body2d.transform.position, body2d.transform.rotation);
    }

    private void RotateBladeTowardImpactPoint(Collision2D other)
    {
        Vector3 myLocation = body2d.transform.position;
        Vector3 targetLocation = other.contacts[0].point;
        targetLocation.z = myLocation.z; // ensure there is no 3D rotation by aligning Z position

        // vector from this object towards the target location
        Vector3 vectorToTarget = targetLocation - myLocation;
        // rotate that vector by 90 degrees around the Z axis
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;

        // get the rotation that points the Z axis forward, and the Y axis 90 degrees away from the target
        // (resulting in the X axis facing the target)
        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);

        // changed this from a lerp to a RotateTowards because you were supplying a "speed" not an interpolation value
        body2d.transform.rotation = targetRotation;
    }

    private void PlaceAxeAtContactPoint(Collision2D other)
    {
        Vector3 pos = body2d.transform.position;
        contactPoint = other.contacts[0];
        pos = other.contacts[0].point;
        pos.z = body2d.transform.position.z;
        body2d.transform.position = pos;
    }
    public void ActivateAbility()
    {
        if (activeAbility == null)
        {
            defaultAbility.Activate(this);
        }
        else
        {
            activeAbility.Activate(this);
            animator.SetBool("hasPower", false);
            activeAbility = null;
        }
    }
    public void Recall()
    {
        returning = true;
        isStuck = false;
        stuckPos = body2d.transform.position;
        SetFlyTrigger(true);
        onBounce.Invoke(body2d.transform.position, body2d.transform.rotation);
        currentRecallTime = 0;
    }
    public void OnCollideWithHolder()
    {
        if (returning)
        {
            grip.HoldAxe();
            body2d.GetComponent<Collider2D>().isTrigger = false;
        }
    }

    public Vector2 GetAxePosition()
    {
        return body2d.transform.position;
    }

    public BoomeraxeGrip GetGrip()
    {
        return grip;
    }
    public void SetHolderPosition(Vector2 pos)
    {
        holderBody2d.transform.position = pos;
    }
    public Vector2 GetContactPointNormal()
    {
        return contactPoint.normal;
    }

    public Vector3 GetHolderBodyPosition()
    {
        return holderBody2d.transform.position;
    }
    public void SetActiveAbility(AxeAbility ability)
    {
        if (returning || grip.IsHoldingAxe())
        {
            return;
        }
        animator.SetBool("hasPower", true);
        activeAbility = ability;
    }
}

