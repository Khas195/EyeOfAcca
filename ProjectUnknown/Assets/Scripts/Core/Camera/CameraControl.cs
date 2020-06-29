using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    CameraFollow follow = null;
    [SerializeField]
    ConstraintCamera constraint = null;
    [SerializeField]
    CameraZoom zoom = null;
    [SerializeField]
    BoomerAxeFollow axeFollow = null;
    [SerializeField]
    FollowAnchorControl anchorControl = null;
    [SerializeField]
    Rigidbody2D axeBody = null;
    [SerializeField]
    [ReadOnly]
    bool followingAxe = false;
    [SerializeField]
    [ReadOnly]
    bool normalZoom = false;
    [SerializeField]
    [ReadOnly]
    bool axeExited = false;

    void Start()
    {
        RecallAbility.RecallEvent.AddListener(ResetAxeExited);
    }
    public void ResetAxeExited()
    {
        axeExited = false;
    }

    void LateUpdate()
    {
        followingAxe = axeFollow.ShouldFollowAxe();
        if (followingAxe && axeExited == false)
        {
            follow.AddEncapsolateObject(axeBody.transform);
            zoom.AddEncapsolateObject(axeBody.transform);
        }
        else
        {
            follow.RemoveEncapsolate(axeBody.transform);
            zoom.RemoveEncapsulateObject(axeBody.transform);
            axeExited = true;

        }
        zoom.UpdateZoom();
        anchorControl.FollowCharacter();

        normalZoom = zoom.IsNormalZoom();
        if (normalZoom)
        {
            anchorControl.HandleLeading();
            anchorControl.HandleLookDown();
        }
        else
        {
            anchorControl.Reset();
        }

        anchorControl.UpdateAnchor();
        follow.Follow();
        constraint.LockCameraToLevel();
    }
}
