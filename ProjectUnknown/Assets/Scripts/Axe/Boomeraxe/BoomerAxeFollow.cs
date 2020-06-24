﻿using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
public class BoomerAxeFollow : MonoBehaviour
{
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    CameraFollow follow = null;

    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    Rigidbody2D characterBody = null;

    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    Rigidbody2D axeBody = null;

    [SerializeField]
    [Required]
    Camera playerCamera = null;

    public void Follow()
    {
        var cameraSizeY = 2 * playerCamera.orthographicSize;
        var cameraSizeX = cameraSizeY * playerCamera.aspect;
        if (IsAxeOutsideCameraBound(cameraSizeY, cameraSizeX) || axeBody.gameObject.activeInHierarchy == false)
        {
            StopFollowAxe();
        }
        else
        {
            FollowAxe();
        }
    }

    private bool IsAxeOutsideCameraBound(float cameraSizeY, float cameraSizeX)
    {
        float horizontalDistance = axeBody.transform.position.x - characterBody.transform.position.x;
        float verticalDistance = axeBody.transform.position.y - characterBody.transform.position.y;

        return Mathf.Abs(horizontalDistance) > cameraSizeX || Mathf.Abs(verticalDistance) > cameraSizeY;
    }

    public void FollowAxe()
    {
        follow.AddEncapsolateObject(axeBody.transform);
    }

    public void StopFollowAxe()
    {
        follow.RemoveEncapsolate(axeBody.transform);
    }
}
