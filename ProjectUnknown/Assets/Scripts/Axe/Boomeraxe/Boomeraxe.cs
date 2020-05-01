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
    [InfoBox("Boomeraxe Datas - The values below are applied for ALL scripts that use this Data Object", EInfoBoxType.Warning)]
    [InfoBox("Boomeraxe Datas - The values below can ONLY be changed by clicking Save in the data object itself", EInfoBoxType.Warning)]
    [DisplayScriptableObjectProperties]
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
    Animator animator = null;

    [BoxGroup("Optional")]
    [SerializeField]
    BallBounceEvent onBounce = new BallBounceEvent();

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

    [SerializeField]
    [ReadOnly]
    float currentRecallTime = 0.0f;
    [SerializeField]
    [ReadOnly]
    Vector2 stuckPos = Vector2.one;
    bool isStuck = false;
    void Start()
    {
        body2d.gravityScale = 0;
    }

    void FixedUpdate()
    {
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
        Vector2 pos = body2d.transform.position;
        currentFlyDirection = (target - pos).normalized;
        returning = false;
        SetFlyTrigger(true);
    }

    private void SetFlyTrigger(bool triggered)
    {
        flyTriggered = triggered;
        animator.SetBool("Flying", flyTriggered);
    }

    public void Reset()
    {
        currentFlyDirection = Vector3.zero;
        body2d.velocity = Vector2.zero;
        returning = false;
        SetFlyTrigger(false);
        isStuck = false;
        onBounce.Invoke(body2d.transform.position, body2d.transform.rotation);
    }

    public void HandleCollision(Collision2D other)
    {
        if (flyTriggered == false) return;
        Vector3 pos = body2d.transform.position;
        pos = other.contacts[0].point;
        pos.z = body2d.transform.position.z;
        body2d.transform.position = pos;
        currentFlyDirection = Vector2.zero;
        body2d.GetComponent<Collider2D>().isTrigger = true;
        isStuck = true;
        SetFlyTrigger(false);
        onBounce.Invoke(body2d.transform.position, body2d.transform.rotation);
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
}
