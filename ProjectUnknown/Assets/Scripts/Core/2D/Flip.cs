using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Flip : MonoBehaviour
{
    [SerializeField]
    [Required]
    Rigidbody2D body = null;
    [SerializeField]
    bool isFacingRight = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var value = body.velocity.x;
        CheckFacing(value);
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
