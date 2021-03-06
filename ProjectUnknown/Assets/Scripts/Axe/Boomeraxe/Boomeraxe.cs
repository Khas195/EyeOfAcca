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
public class AxeTransformEvent : UnityEvent<Vector3, Quaternion>
{

}
[Serializable]
public class AxeAbilityEvent : UnityEvent<AxeAbility>
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
    SpriteRenderer axeSprite = null;

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
    AxeTransformEvent OnStuck = new AxeTransformEvent();


    [BoxGroup("Optional")]
    [SerializeField]
    AxeTransformEvent OnRecall = new AxeTransformEvent();


    [BoxGroup("Optional")]
    [SerializeField]
    AxeAbilityEvent useAbilityEvent = new AxeAbilityEvent();


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

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    ContactPoint2D contactPoint;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    Collider2D stuckObject = null;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    Vector2 offsetWithStuckSurface;



    [BoxGroup("Current Status")]
    [SerializeField]
    AudioSource spinningSource = null;

    void Awake()
    {
        body2d.gravityScale = 0;
        Reset();
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(holderBody2d.transform.position, datas.recallTimeBaseOnDistance[0].time);
        Gizmos.DrawWireSphere(holderBody2d.transform.position, datas.recallTimeBaseOnDistance[datas.recallTimeBaseOnDistance.length - 1].time);
    }
    void FixedUpdate()
    {

        animator.SetBool("isStuck", isStuck);
        animator.SetBool("Recall", returning);
        if (flyTriggered)
        {
            if (returning)
            {
                if (currentRecallTime > datas.recallTime)
                {
                    this.OnCollideWithHolder();
                    return;
                }
                body2d.transform.position = Vector3.Lerp(stuckPos, holderBody2d.transform.position, datas.recallCurve.Evaluate(currentRecallTime));
                currentRecallTime += Time.deltaTime;
            }
            else
            {
                Vector2 pos = body2d.transform.position;
                body2d.MovePosition(pos + currentFlyDirection * datas.flyVelocity * Time.fixedDeltaTime);
            }
        }
        else
        {
            if (isStuck)
            {
                if (stuckObject)
                {
                    Vector2 stuckObjCurPos = stuckObject.transform.position;
                    body2d.transform.position = stuckObjCurPos - offsetWithStuckSurface;
                }
            }
        }
    }

    public bool IsStuck()
    {
        return isStuck;
    }
    public void Fly(Vector2 target, Vector2 startPos)
    {
        LogHelper.GetInstance().Log("Player ".Bolden().Colorize(Color.green) + "has thrown the " + "Boomeraxe".Bolden().Colorize("#83ecd7"), true);


        body2d.gameObject.SetActive(true);
        Vector2 pos = startPos;
        currentFlyDirection = (target - pos).normalized;
        body2d.transform.position = startPos;
        returning = false;

        axeSprite.sortingLayerName = "AxeBack";

        SetFlyTrigger(true);
    }

    private void SetFlyTrigger(bool triggered)
    {
        flyTriggered = triggered;
        if (triggered)
        {
            SFXSystem.GetInstance().PlaySound(SFXResources.SFXList.axeSpinning, spinningSource);
        }
        else
        {
            spinningSource.Stop();
        }
    }

    public void Reset()
    {
        spinningSource.Stop();
        body2d.gameObject.SetActive(false);
        currentFlyDirection = Vector3.zero;
        body2d.velocity = Vector2.zero;
        returning = false;
        SetFlyTrigger(false);
        isStuck = false;
        stuckObject = null;
        this.currentRecallTime = 0.0f;
        body2d.GetComponent<Collider2D>().isTrigger = false;
    }

    public Collider2D GetStuckCollider()
    {
        return stuckObject;
    }

    public void HandleCollision(Collision2D other)
    {
        if (flyTriggered == false)
        {
            LogHelper.GetInstance().Log(("Axe touched " + other + "but fly is not triggerd ").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.Console);
            return;
        }

        Vector2 pos = body2d.transform.position;
        pos = (other.contacts[0].point - pos).normalized;
        if (Vector2.Dot(pos, currentFlyDirection) < -0.8)
        {
            LogHelper.GetInstance().Log(("Axe touched " + other + "but obj is behind the axe - Dot Product: " + Vector2.Dot(pos, currentFlyDirection)).Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.Console);
            return;
        }

        contactPoint = other.contacts[0];
        stuckObject = other.collider;
        LogHelper.GetInstance().Log("*THUD*".Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);

        RotateBladeTowardImpactPoint(other);

        body2d.GetComponent<Collider2D>().isTrigger = true;
        SetFlyTrigger(false);

        isStuck = true;

        spinningSource.Stop();

        Vector2 stuckObj = other.collider.transform.position;
        Vector2 axePos = body2d.transform.position;
        offsetWithStuckSurface = (stuckObj - axePos);

        currentFlyDirection = Vector2.zero;
        OnStuck.Invoke(contactPoint.point, body2d.transform.rotation);
    }

    private void RotateBladeTowardImpactPoint(Collision2D other)
    {
        Vector3 myLocation = body2d.transform.position;
        Vector3 targetLocation = other.contacts[0].point;
        targetLocation.z = myLocation.z; // ensure there is no 3D rotation by aligning Z position

        Vector3 vectorToTarget = targetLocation - myLocation;

        // rotate that vector by 90 degrees around the Z axis
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;

        // get the rotation that points the Z axis forward, and the Y axis 90 degrees away from the target
        // (resulting in the X axis facing the target)
        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);

        // changed this from a lerp to a RotateTowards because you were supplying a "speed" not an interpolation value
        body2d.transform.rotation = targetRotation;
    }
    public void ActivateAbility()
    {
        if (activeAbility == null)
        {
            defaultAbility.Activate(this);
            useAbilityEvent.Invoke(defaultAbility);
        }
        else
        {
            activeAbility.Activate(this);
            useAbilityEvent.Invoke(activeAbility);
            animator.SetBool(activeAbility.GetAbilityPower(), false);
            activeAbility = null;
        }
        useAbilityEvent.RemoveAllListeners();
    }
    public void Recall()
    {
        animator.SetBool("Recall", false);
        returning = true;
        isStuck = false;
        stuckPos = body2d.transform.position;
        body2d.gameObject.SetActive(true);
        SetFlyTrigger(true);
        body2d.GetComponent<Collider2D>().isTrigger = true;
        currentRecallTime = 0;
        axeSprite.sortingLayerName = "AxeFront";
        CalculateRecallDistance();

        OnRecall.Invoke(contactPoint.point, body2d.transform.rotation);

    }

    private void CalculateRecallDistance()
    {
        var axeToHolderDistance = Vector2.Distance(body2d.transform.position, holderBody2d.transform.position);
        var newKey = new Keyframe(datas.recallTimeBaseOnDistance.Evaluate(axeToHolderDistance), 1);
        newKey.inTangent += 10;
        datas.recallCurve.MoveKey(datas.recallCurve.length - 1, newKey);
    }

    public void OnCollideWithHolder()
    {
        if (returning)
        {
            grip.HoldAxe();
        }
    }

    public Vector2 GetAxePosition()
    {
        return body2d.transform.position;
    }
    public Transform GetAxeTransform()
    {
        return body2d.transform;
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
    public bool SetActiveAbility(AxeAbility ability, UnityAction<AxeAbility> callback = null)
    {
        if (IsInThrowMotion() == false)
        {
            return false;
        }

        if (ability != null)
        {
            animator.SetBool(ability.GetAbilityPower(), true);
        }
        activeAbility = ability;

        AddActiveAbilityCallback(callback);

        return true;
    }

    public bool IsInThrowMotion()
    {
        return !(returning || grip.IsHoldingAxe());
    }

    public void AddActiveAbilityCallback(UnityAction<AxeAbility> callback = null)
    {
        if (callback == null) return;
        // Remove the callback fisrt to make sure there is no duplicate cause it being called twice.
        useAbilityEvent.RemoveListener(callback);
        useAbilityEvent.AddListener(callback);
    }
    public Transform GetHolder()
    {
        return holderBody2d.transform;
    }

    public void NullAbility()
    {
        if (activeAbility)
        {
            animator.SetBool(activeAbility.GetAbilityPower(), false);
        }
        spinningSource.Stop();
        activeAbility = null;
        useAbilityEvent.Invoke(activeAbility);
        useAbilityEvent.RemoveAllListeners();
        ActivateAbility();
    }
    public bool HasActiveAbility()
    {
        return activeAbility != null;
    }
    public AxeAbility GetCurrentAbility()
    {
        if (activeAbility != null)
        {
            return activeAbility;
        }
        else
        {
            return defaultAbility;
        }
    }
    public void SetStruck(bool stuck)
    {
        isStuck = stuck;
    }
}

