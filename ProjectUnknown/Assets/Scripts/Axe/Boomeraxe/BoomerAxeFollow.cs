using System.Collections;
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

    void Update()
    {
        var cameraSizeY = 2 * playerCamera.orthographicSize;
        var cameraSizeX = cameraSizeY * playerCamera.aspect;
        if (Mathf.Abs(axeBody.transform.position.x - characterBody.transform.position.x) > cameraSizeX / 1.5f || Mathf.Abs(axeBody.transform.position.y - characterBody.transform.position.y) > cameraSizeY / 2.0f)
        {
            follow.RemoveEncapsolate(axeBody.transform);
        }
        else
        {
            follow.AddEncapsolateObject(axeBody.transform);
        }
    }

}
