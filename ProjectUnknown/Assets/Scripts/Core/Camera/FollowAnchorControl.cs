using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class FollowAnchorControl : MonoBehaviour
{
    [SerializeField]
    [Required]
    Transform followAnchor = null;

    [SerializeField]
    [Required]
    Transform followPoint = null;
    [SerializeField]
    CameraFollow follow = null;
    [SerializeField]
    [Required]
    GameStateSnapShot snap = null;
    Vector3 mousePosition = Vector3.zero;
    [SerializeField]
    float standTime = 2f;
    [SerializeField]
    float distance = 5f;
    float curTime = 0;
    [SerializeField]
    Camera playerCam = null;
    Vector3 cachedPos = Vector3.zero;
    void Start()
    {
        cachedPos = followPoint.localPosition;
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(followAnchor.position, followPoint.position);
        Gizmos.DrawWireSphere(followAnchor.position, 1f);
        Gizmos.DrawWireSphere(followPoint.position, 1f);
        Gizmos.DrawLine(followAnchor.position, mousePosition);
        Gizmos.DrawWireSphere(mousePosition, 1f);
        Gizmos.DrawWireSphere(followAnchor.position, distance);
    }
    void LateUpdate()
    {
        followAnchor.position = snap.CharacterPosition;
        if (snap.CharacterVelocity.x == 0 || follow.IsHoning() == false)
        {
            curTime += Time.deltaTime;
        }
        else
        {
            curTime = 0;
            followPoint.localPosition = cachedPos;
            followAnchor.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
        if (curTime > standTime)
        {
            var mousPos = playerCam.ScreenToWorldPoint(Input.mousePosition);
            mousPos.z = 0;
            var dir = mousPos - followAnchor.position;
            if (Vector2.Distance(mousPos, followAnchor.position) >= distance)
            {
                followPoint.position = followAnchor.transform.position + dir.normalized * distance;
            }
            else
            {
                followPoint.position = mousPos;
            }
        }


    }



}
