using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    CameraFollow follow = null;
    [SerializeField]
    ConstraintCamera constraint = null;
    [SerializeField]
    BoomerAxeFollow axeFollow = null;


    // Update is called once per frame
    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        follow.Follow();
        axeFollow.Follow();
        constraint.LockCameraToLevel();
    }
}
