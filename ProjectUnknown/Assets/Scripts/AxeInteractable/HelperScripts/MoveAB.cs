using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class MoveAB : MonoBehaviour
{
    public enum MoveABEnum
    {
        A,
        B,
        Middle
    }

    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    Rigidbody2D body2d = null;

    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    Transform aPosition = null;
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    Transform bPosition = null;

    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    BoxCollider2D box = null;

    [BoxGroup("Settings")]
    [SerializeField]
    bool automaticallyMoveBackToStart = false;



    [BoxGroup("Settings")]
    [SerializeField]
    Tweener.TweenType moveType = Tweener.TweenType.EaseOutQuad;


    [BoxGroup("Settings")]
    [SerializeField]
    bool allowInteractionDuringMoveBack = false;



    [BoxGroup("Settings")]
    [SerializeField]
    float timeTillDestinationReached = 1;

    [BoxGroup("Settings")]
    [ShowIf("automaticallyMoveBackToStart")]
    [SerializeField]
    float moveBackTime = 1;

    [BoxGroup("Settings")]
    [SerializeField]
    [OnValueChanged("OnStartPositionChanged")]
    MoveABEnum startPos = MoveABEnum.Middle;


    [BoxGroup("Settings")]
    [SerializeField]
    public UnityEvent OnReachA = new UnityEvent();

    [BoxGroup("Settings")]
    [SerializeField]
    public UnityEvent OnReachB = new UnityEvent();
    [BoxGroup("Settings")]
    [SerializeField]
    public UnityEvent OnReached = new UnityEvent();

    [BoxGroup("Settings")]
    [SerializeField]
    public UnityEvent OnReturn = new UnityEvent();


    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    bool inMotion = false;
    [BoxGroup("Current Status")]
    [SerializeField]
    [ShowIf("automaticallyMoveBackToStart")]
    [ReadOnly]
    bool isMovingBack = false;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    float curTime = 0.0f;
    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    float currentTimeTillDestinationReaded = 0.0f;



    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    private Vector2 originPos = Vector2.one;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    private Vector2 destination = Vector2.one;


    public void OnStartPositionChanged()
    {
        GoToStartPosition();
    }

    public Vector2 GetPosition(MoveABEnum a)
    {
        if (a == MoveABEnum.A)
        {
            return aPosition.position;
        }
        else if (a == MoveABEnum.B)
        {
            return bPosition.position;
        }
        else
        {
            return (aPosition.position + bPosition.position) / 2;
        }
    }

    public void GoToStartPosition()
    {
        if (startPos == MoveABEnum.A)
        {
            box.transform.position = aPosition.position;
        }
        else if (startPos == MoveABEnum.B)
        {
            box.transform.position = bPosition.position;
        }
        else
        {
            box.transform.position = (bPosition.position + aPosition.position) / 2;
        }
    }
    void OnDrawGizmos()
    {
        if (aPosition == null || bPosition == null)
        {
            return;
        }
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(aPosition.position, box.bounds.extents * 2);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bPosition.position, box.bounds.extents * 2);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(aPosition.position, bPosition.position);

    }

    public bool IsCurrentDestination(MoveABEnum destinationEnum)
    {
        return destination == GetPosition(destinationEnum);
    }

    void FixedUpdate()
    {
        if (inMotion)
        {
            body2d.MovePosition(Tweener.Tween(moveType, curTime, originPos, destination, currentTimeTillDestinationReaded));
            curTime += Time.fixedDeltaTime;
            if (HasReachedDestination())
            {
                Vector2 posA = aPosition.position;
                Vector2 posB = bPosition.position;
                if (IsAt(MoveABEnum.A))
                {
                    OnReached.Invoke();
                    OnReachA.Invoke();
                }
                else if (IsAt(MoveABEnum.B))
                {
                    OnReached.Invoke();
                    OnReachB.Invoke();
                }

                if (automaticallyMoveBackToStart && IsAt(startPos) == false)
                {
                    GoTo(startPos);
                    isMovingBack = true;
                    currentTimeTillDestinationReaded = moveBackTime;
                    OnReturn.Invoke();
                }
                else
                {
                    isMovingBack = false;
                    inMotion = false;
                    this.box.transform.position = destination;
                }
                curTime = 0;
            }
        }
    }

    public bool HasReachedDestination()
    {
        return Vector2.Distance(box.transform.position, destination) <= 0.05f || curTime >= currentTimeTillDestinationReaded || inMotion == false;
    }

    public void GoTo(MoveABEnum moveDestination)
    {
        LogHelper.GetInstance().Log("Here".Bolden());
        if (allowInteractionDuringMoveBack == false && isMovingBack) return;

        if (moveDestination == MoveABEnum.A)
        {
            destination = aPosition.position;
        }
        else if (moveDestination == MoveABEnum.B)
        {
            destination = bPosition.position;
        }
        else
        {
            destination = (aPosition.position + bPosition.position) / 2;
        }
        originPos = box.transform.position;
        currentTimeTillDestinationReaded = timeTillDestinationReached;
        curTime = 0;
        inMotion = true;
    }
    public bool IsAt(MoveABEnum posEnum)
    {
        if (posEnum == MoveABEnum.A)
        {
            return Vector2.Distance(box.transform.position, aPosition.position) < 0.05f;
        }
        else if (posEnum == MoveABEnum.B)
        {

            return Vector2.Distance(box.transform.position, bPosition.position) < 0.05f;
        }
        else
        {
            return Vector2.Distance(box.transform.position, (aPosition.position + bPosition.position) / 2) < 0.05f;
        }
    }
    public MoveABEnum GetStartPos()
    {
        return startPos;
    }
    public bool IsInMotion()
    {
        return inMotion;
    }
    public void SetMoveTime(float moveTime)
    {
        this.timeTillDestinationReached = moveTime;
    }

    public void SetReturnTime(float returnTime)
    {
        this.moveBackTime = returnTime;
    }
}
