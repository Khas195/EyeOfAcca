using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Movement2DPlatform : IMovement
{
    [BoxGroup("Settings")]
    [SerializeField]
    UnityEvent jumpEvent = new UnityEvent();

    [BoxGroup("Settings")]
    [SerializeField]
    [Required]
    [ShowIf("noCharacter")]
    Rigidbody2D body2D = null;

    [BoxGroup("Settings")]
    [SerializeField]
    Vector2 checkGroundBoxSize = Vector2.one;
    [BoxGroup("Settings")]
    [SerializeField]
    Vector3 checkGroundBoxOffset = Vector3.one;
    [BoxGroup("Settings")]
    [SerializeField]
    LayerMask jumpableLayer;

    [BoxGroup("Settings")]
    [SerializeField]
    Vector3 headColSize = Vector3.one;

    [BoxGroup("Settings")]
    [SerializeField]
    Vector3 headColOffset = Vector3.one;



    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    float timeScale = 1.0f;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    bool jumpSignal = false;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    float cachedSide = 0;


    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    bool jumpTriggered = false;
    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    bool isAccelUp = false;
    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]

    int moveDirHorizontal = 0;
    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]

    float previousSide = 0;
    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]

    float curVelTime = 0.0f;
    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]

    AnimationCurve currentVelCurve = null;
    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]

    float currentJumpBufferTime = 0.0f;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(body2D.transform.position + checkGroundBoxOffset, checkGroundBoxSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(headColOffset + body2D.transform.position, headColSize);

    }

    public override void Move(float forward, float side)
    {
        previousSide = cachedSide;
        cachedSide = side;
        if (Mathf.Approximately(previousSide, 0) == false && Mathf.Approximately(cachedSide, 0))
        {
            currentVelCurve = data.decelCurve;
            curVelTime = 0;
        }
        else if (Mathf.Approximately(previousSide, 0) == true && Mathf.Approximately(cachedSide, 0) == false)
        {
            currentVelCurve = data.accelCurve;
            curVelTime = 0;
        }
        if (cachedSide > 0)
        {
            moveDirHorizontal = 1;
        }
        else if (cachedSide < 0)
        {
            moveDirHorizontal = -1;
        }
    }

    public bool GetJumpTriggered()
    {
        return jumpTriggered;
    }

    public override bool CanJump()
    {
        bool temp = (jumpTriggered == false && currentJumpBufferTime <= data.bufferTimeForJump);
        if (this.IsTouchingGround())
        {
            return true;
        }
        else
        {
            return temp;
        }
    }
    void FixedUpdate()
    {
    }
    void Update()
    {

        ProcessMovement();
        if (isAccelUp == false)
        {
            if (this.IsTouchingGround() == false)
            {
                currentJumpBufferTime += Time.deltaTime;
            }
            else
            {
                jumpTriggered = false;
                currentJumpBufferTime = 0;
            }
        }

        if (jumpSignal)
        {
            this.Jump();
            jumpSignal = false;
        }

        if (isAccelUp)
        {
            body2D.velocity += Vector2.up * data.jumpGravity * Time.deltaTime;
            if (body2D.velocity.y <= 0)
            {
                StartFalling();
            }
        }
        if (isAccelUp == false)
        {
            body2D.velocity += Vector2.up * data.fallGravity * Time.deltaTime;
        }
        body2D.velocity *= timeScale;
        data.currentVelocity = body2D.velocity;
    }
    public override void StartFalling()
    {
        isAccelUp = false;
    }
    private void Jump()
    {
        jumpEvent.Invoke();
        jumpTriggered = true;
        currentJumpBufferTime = data.bufferTimeForJump + 1;
        var vel = this.body2D.velocity;
        vel.y = data.initialJumpVelocity;
        this.body2D.velocity = vel;
        isAccelUp = true;
    }

    public void SetTimeScale(float scale)
    {
        timeScale = scale;
    }
    private void ProcessMovement()
    {
        var curVel = body2D.velocity;

        if (this.currentVelCurve != null)
        {
            curVel.x = this.currentVelCurve.Evaluate(curVelTime) * moveDirHorizontal;
        }
        else
        {
            curVel.x = 0;
        }

        body2D.velocity = curVel;
        curVelTime += Time.deltaTime;
    }

    public override void SignalJump()
    {
        jumpSignal = true;
    }
    public override void SetRigidBody(Rigidbody2D body)
    {
        this.body2D = body;
    }
    public override bool IsTouchingGround()
    {
        if (GetGroundCollider2D() != null)
        {
            return true;
        }
        return false;
    }
    public float GetDesiredMovementHorizontal()
    {
        return cachedSide;
    }
    public override Collider2D[] GetGroundCollider2D()
    {

        var cols = Physics2D.OverlapBoxAll(this.body2D.transform.position + checkGroundBoxOffset, checkGroundBoxSize, 0, jumpableLayer);
        if (cols.Length > 0)
        {
            return cols;
        }
        else
        {
            return null;
        }
    }


    public override void SetRigidBody(Rigidbody hostRigidBody)
    {
        base.SetRigidBody(hostRigidBody);
    }

    public override MovementData GetMovementData()
    {
        return base.GetMovementData();
    }

    public override void RotateToward(Vector3 direction, bool rotateY)
    {
        base.RotateToward(direction, rotateY);
    }


    public override float GetCurrentSpeed()
    {
        return this.moveDirHorizontal;
    }


}
