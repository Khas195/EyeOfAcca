using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    CameraFollow follow;
    [SerializeField]
    ConstraintCamera constraint;
    [SerializeField]
    BoomerAxeFollow axeFollow;


    // Update is called once per frame
    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        axeFollow.FollowAxe();
        follow.Follow();
        constraint.LockCameraToLevel();
    }
}
