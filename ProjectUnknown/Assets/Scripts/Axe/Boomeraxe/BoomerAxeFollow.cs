using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
public class BoomerAxeFollow : MonoBehaviour
{
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    CameraFollow follow;

    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    Rigidbody2D characterBody;

    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    Rigidbody2D axeBody;

    [BoxGroup("Settings")]
    [SerializeField]
    [Required]
    CameraSettings settings;
    [SerializeField]
    [Required]
    Camera playerCamera = null;

    void Update()
    {
        var cameraSizeY = 2 * playerCamera.orthographicSize;
        var cameraSizeX = cameraSizeY * playerCamera.aspect;
        if (Mathf.Abs(axeBody.transform.position.x - characterBody.transform.position.x) > cameraSizeX || Mathf.Abs(axeBody.transform.position.y - characterBody.transform.position.y) > cameraSizeY)
        {
            follow.RemoveEncapsolate(axeBody.transform);
        }
        else
        {
            follow.AddEncapsolateObject(axeBody.transform);
        }
    }

}
