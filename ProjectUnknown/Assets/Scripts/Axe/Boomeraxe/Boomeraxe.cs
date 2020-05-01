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
    Rigidbody2D holderBody2d = null;

    [BoxGroup("Requirement")]
    [SerializeField]
    [Required]
    BoomeraxeGrip grip = null;



    [BoxGroup("Requirement")]
    [SerializeField]
    Animator animator = null;

    [BoxGroup("Optional")]
    [SerializeField]
    BallBounceEvent onBounce = new BallBounceEvent();


    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    int bounceCount = 0;

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


    bool doOnce = true;


    void Start()
    {
        body2d.gravityScale = 0;
    }

    void FixedUpdate()
    {
        if (flyTriggered)
        {
            Vector2 pos = body2d.transform.position;
            body2d.MovePosition(pos + currentFlyDirection * datas.flyVelocity * Time.fixedDeltaTime);
            body2d.velocity = Vector2.ClampMagnitude(body2d.velocity, datas.flyVelocity);
            if (doOnce && Vector2.Distance(holderBody2d.transform.position, body2d.transform.position) > datas.flyDistance)
            {
                SeekCharacter();
                returning = true;
                doOnce = false;
            }
        }
    }

    private void SeekCharacter()
    {
        LogHelper.GetInstance().Log(("It's comming back!!!").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
        currentFlyDirection = holderBody2d.transform.position - body2d.transform.position;
        currentFlyDirection.Normalize();
        returning = true;
    }

    public void Fly(Vector2 target)
    {
        LogHelper.GetInstance().Log("Player ".Bolden().Colorize(Color.green) + "has thrown the " + "Boomeraxe".Bolden().Colorize("#83ecd7"), true);
        Vector2 pos = body2d.transform.position;
        currentFlyDirection = (target - pos).normalized;
        flyTriggered = true;
        doOnce = true;
        returning = false;
        animator.SetBool("Flying", flyTriggered);
    }

    public void Reset()
    {
        currentFlyDirection = Vector3.zero;
        flyTriggered = false;
        returning = false;
        doOnce = true;
        bounceCount = 0;
        body2d.velocity = Vector2.zero;
        animator.SetBool("Flying", flyTriggered);
        onBounce.Invoke(body2d.transform.position, body2d.transform.rotation);
    }

    public int GetBounceCount()
    {
        return bounceCount;
    }

    public void HandleOnTriggerEnter(Collider2D other)
    {
        if (flyTriggered == false) return;
        LogHelper.GetInstance().Log("Returning after touch" + other, true);
        SeekCharacter();
        onBounce.Invoke(body2d.transform.position, body2d.transform.rotation);
    }
    public void OnCollideWithHolder()
    {
        if (returning)
        {
            grip.HoldAxe();
        }
    }
    public Vector2 GetFlyDirection()
    {
        return currentFlyDirection;
    }
}
