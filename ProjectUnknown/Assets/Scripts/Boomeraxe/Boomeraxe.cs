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
    bool flyingToTarget = false;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    bool returning = false;


    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    Vector2 originPoint = Vector2.zero;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    Vector2 currentFlyDirection = Vector2.zero;


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(originPoint, datas.flyDistance);
    }
    // Start is called before the first frame update
    void Start()
    {
        body2d.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (flyTriggered)
        {
            body2d.velocity = currentFlyDirection * datas.flyVelocity;
            body2d.velocity = Vector2.ClampMagnitude(body2d.velocity, datas.flyVelocity);
            if (Vector2.Distance(originPoint, body2d.transform.position) > datas.flyDistance)
            {
                if (flyingToTarget)
                {
                    LogHelper.GetInstance().Log("Boomeraxe".Bolden().Colorize("#83ecd7") + " has reached max flying distance, now returning", true);
                    currentFlyDirection *= -1;
                    flyingToTarget = false;
                    returning = true;
                }
                else
                {
                    if (returning == false)
                    {
                        LogHelper.GetInstance().Log("Boomeraxe".Bolden().Colorize("#83ecd7") + " has reached max flying distance, had bounced before -> teleport back to holder", true);
                        grip.SetAxeCatchable(true);
                        grip.HoldAxe();
                    }
                }
            }
        }
    }

    public void Fly(Vector2 target)
    {
        LogHelper.GetInstance().Log("Player ".Bolden().Colorize(Color.green) + "has thrown the " + "Boomeraxe".Bolden().Colorize("#83ecd7"), true);
        originPoint = body2d.transform.position;
        currentFlyDirection = (target - originPoint).normalized;
        flyTriggered = true;
        flyingToTarget = true;
        animator.SetBool("Flying", flyTriggered);
    }

    public void Reset()
    {
        currentFlyDirection = Vector3.zero;
        flyTriggered = false;
        bounceCount = 0;
        body2d.velocity = Vector2.zero;
        animator.SetBool("Flying", flyTriggered);
        onBounce.Invoke(body2d.transform.position, body2d.transform.rotation);
    }

    public int GetBounceCount()
    {
        return bounceCount;
    }

    public void HandleCollisionWith(Collision2D other)
    {
        if (flyTriggered == false) return;
        LogHelper.GetInstance().Log("Boomeraxe".Bolden().Colorize("#83ecd7") + " hit an object");
        bounceCount += 1;
        originPoint = other.contacts[0].point;
        flyingToTarget = false;
        returning = false;
        onBounce.Invoke(body2d.transform.position, body2d.transform.rotation);
        Reflect(other);
    }

    private void Reflect(Collision2D other)
    {
        Vector2 inDir = currentFlyDirection;
        Vector2 outDir = Vector2.Reflect(currentFlyDirection, other.contacts[0].normal);
        currentFlyDirection = outDir;
        LogHelper.GetInstance().Log("Boomeraxe".Bolden().Colorize("#83ecd7") + " Reflect off object with inDir: " + inDir + " | outDir: " + outDir);
    }
}
