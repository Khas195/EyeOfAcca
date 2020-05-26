using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePivotBasedOnVelocity : MonoBehaviour
{
    [SerializeField]
    CameraFollow follow = null;
    [SerializeField]
    GameObject pivot = null;
    [SerializeField]
    Movement2DPlatform movement = null;
    [SerializeField]
    Rigidbody2D body2d = null;
    [SerializeField]
    float moveTime = 0.5f;
    [SerializeField]
    float curTime = 0;
    [SerializeField]
    Vector2 targetPos = Vector2.one;
    [SerializeField]
    Vector2 beginPos = Vector2.one;
    [SerializeField]
    Vector2 currentVel = Vector2.one;
    bool isMoving = false;
    void OnEnable()
    {
        targetPos = pivot.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (movement.IsTouchingGround() == false && body2d.velocity.y < 0)
        {
            follow.AddEncapsolateObject(pivot.transform);
        }
        else
        {
            follow.RemoveEncapsolate(pivot.transform);
        }

    }
}
