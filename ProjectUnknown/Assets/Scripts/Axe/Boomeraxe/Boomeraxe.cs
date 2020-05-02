using System;
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
    Rigidbody2D holderBody2d = null;


    [BoxGroup("Requirement")]
    [SerializeField]
    [Required]
    Transform bladePivot = null;

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
        Vector2 pos = body2d.transform.position;
        currentFlyDirection = (target - pos).normalized;

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
        PlaceAxeAtContactPoint(other);

        Vector2 bladeDir = (bladePivot.position - holderBody2d.transform.position).normalized;

        Vector2 holdPos = holderBody2d.transform.position;
        Vector2 impactDir = (other.contacts[0].point - holdPos).normalized;

        float angle = Vector2.Angle(bladeDir, impactDir);

        body2d.transform.rotation = Quaternion.Euler(0, 0, angle);

        body2d.GetComponent<Collider2D>().isTrigger = true;
        SetFlyTrigger(false);
        isStuck = true;
        currentFlyDirection = Vector2.zero;
        onBounce.Invoke(body2d.transform.position, body2d.transform.rotation);
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
    public Vector2 GetFlyDirection()
    {
        return currentFlyDirection;
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
        activeAbility = ability;
    }
}

