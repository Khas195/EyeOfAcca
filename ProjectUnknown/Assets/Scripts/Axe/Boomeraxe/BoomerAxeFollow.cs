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

    void OnDrawGizmos()
    {
        if (characterBody == null || settings == null)
        {
            return;
        }
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(characterBody.transform.position, settings.maxCameraFollwAxeDistance);
    }
    void Update()
    {
        if (Vector2.Distance(characterBody.transform.position, axeBody.transform.position) >= settings.maxCameraFollwAxeDistance)
        {
            follow.RemoveEncapsolate(axeBody.transform);
        }
        else
        {
            follow.AddEncapsolateObject(axeBody.transform);
        }
    }

}
