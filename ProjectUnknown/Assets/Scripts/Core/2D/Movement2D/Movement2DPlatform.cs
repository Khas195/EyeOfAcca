using System;
using NaughtyAttributes;
using UnityEngine;

public class Movement2DPlatform : IMovement
{
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
    float accelrationTimeCounter = 0.0f;
    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    float cachedSide = 0;
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(body2D.transform.position + checkGroundBoxOffset, checkGroundBoxSize);
    }
    public override void Move(float forward, float side)
    {
        cachedSide = side;
    }

    public bool GetJumpTriggered()
    {
        return jumpTriggered;
    }
    bool jumpTriggered = false;
    void Update()
    {
        jumpTriggered = false;
        ProcessMovement();
        if (jumpSignal)
        {
            if (this.IsTouchingGround())
            {
                jumpTriggered = true;
                var curVel = body2D.velocity;
                curVel.y = data.jumpForce;
                body2D.velocity = curVel;
            }
            jumpSignal = false;
        }
        if (body2D.velocity.y < 0)
        {
            body2D.velocity += Vector2.up * Physics2D.gravity.y * (data.fallMultiplier - 1) * Time.deltaTime;
        }
        body2D.velocity *= timeScale;

    }

    public void SetTimeScale(float scale)
    {
        timeScale = scale;
    }
    private void ProcessMovement()
    {
        if (cachedSide == 0)
        {
            accelrationTimeCounter = 0;
        }
        var curVel = body2D.velocity;
        var speed = Tweener.EaseInQuad(accelrationTimeCounter, 0, data.runSpeed, data.timeTilMaxSpeed);
        if (speed >= data.runSpeed)
        {
            speed = data.runSpeed * cachedSide;
        }
        else
        {
            speed *= cachedSide;
        }
        curVel.x = speed;
        body2D.velocity = curVel;
        accelrationTimeCounter += Time.deltaTime;
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
        var cols = Physics2D.OverlapBoxAll(this.body2D.transform.position + checkGroundBoxOffset, checkGroundBoxSize, 0, jumpableLayer);
        if (cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                LogHelper.GetInstance().Log(body2D.name.Bolden() + " is standing on " + cols[i].name.Bolden());
            }
            return true;
        }
        return false;
    }
    public float GetDesiredMovementHorizontal()
    {
        return cachedSide;
    }

}
