using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Flip : MonoBehaviour
{
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    bool useVelocity = true;

    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    [ShowIf("useVelocity")]
    Rigidbody2D body = null;
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    [HideIf("useVelocity")]
    IMovement movement = null;

    [BoxGroup("Current Status")]
    [SerializeField]
    bool isFacingRight = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (useVelocity)
        {
            var value = body.velocity.x;
            CheckFacing(value);
        }
        else
        {
            var value = movement.GetCurrentSpeed();
            CheckFacing(value);

        }
    }

    public void CheckFacing(float value)
    {
        if (value < 0 && isFacingRight)
        {
            FlipModel();
        }
        else if (value > 0 && isFacingRight == false)
        {
            FlipModel();
        }
    }

    public void FlipModel()
    {
        isFacingRight = !isFacingRight;
        var localScale = body.transform.localScale;
        localScale.x *= -1;
        body.transform.localScale = localScale;
    }

    public bool IsFacingRight()
    {
        return isFacingRight;
    }
}
