using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    Transform host = null;
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    Transform character = null;

    [BoxGroup("Settings")]
    [SerializeField]
    [Required]
    CameraSettings settings = null;
    [BoxGroup("Settings")]
    [SerializeField]
    bool followX = false;
    [BoxGroup("Settings")]
    [SerializeField]
    bool followY = false;
    [BoxGroup("Settings")]
    [SerializeField]
    bool followZ = false;
    [BoxGroup("Current Status")]
    [SerializeField]
    List<Transform> encapsolatedTarget = new List<Transform>();

    bool honeIn = false;

    // Start is called before the first frame update
    void Start()
    {
        if (character != null)
        {
            encapsolatedTarget.Add(character);
        }
        var master = GameMaster.GetInstance(false);
        if (master)
        {
            this.SetPosition(master.GetSpawnLocation());
        }
    }

    public bool IsHoning()
    {
        return honeIn;
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(host.position, settings.cameraFollowDeadZoneBoxSize);
        var targetPos = GetCenterPosition(encapsolatedTarget);
        Gizmos.DrawWireSphere(targetPos, 1f);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(host.position, 0.5f);
    }

    public Camera GetCamera()
    {
        return this.host.GetComponentInChildren<Camera>();
    }

    void LateUpdate()
    {
        var targetPos = GetCenterPosition(encapsolatedTarget);
        var hostPos = host.position;

        var rightSide = hostPos.x + settings.cameraFollowDeadZoneBoxSize.x / 2;
        var leftSide = hostPos.x - settings.cameraFollowDeadZoneBoxSize.x / 2;
        var topSide = hostPos.y + settings.cameraFollowDeadZoneBoxSize.y / 2;
        var bottomSide = hostPos.y - settings.cameraFollowDeadZoneBoxSize.y / 2;
        if (targetPos.x < leftSide || targetPos.x > rightSide)
        {
            honeIn = true;
        }
        if (targetPos.y < bottomSide || targetPos.y > topSide)
        {
            honeIn = true;
        }

        if (honeIn)
        {
            if (followX)
            {
                hostPos.x = Mathf.Lerp(hostPos.x, targetPos.x, settings.cameraSpeed * Time.deltaTime);
            }
            if (followY)
            {
                hostPos.y = Mathf.Lerp(hostPos.y, targetPos.y, settings.cameraSpeed * Time.deltaTime);
            }

            if (followZ)
            {
                hostPos.z = Mathf.Lerp(hostPos.z, targetPos.z, settings.cameraSpeed * Time.deltaTime);
            }
        }

        if (Vector2.Distance(targetPos, hostPos) <= 0.5f)
        {
            honeIn = false;
        }
        host.transform.position = hostPos;
    }

    public void RemoveEncapsolate(Transform transform)
    {
        if (encapsolatedTarget.Contains(transform))
        {
            encapsolatedTarget.Remove(transform);
        }
    }

    public void SetFollowPercentage(float value)
    {
        settings.cameraSpeed = value;
    }

    public void Clear(bool clearPlayer)
    {
        encapsolatedTarget.Clear();
        if (clearPlayer == false)
        {
            encapsolatedTarget.Add(character);
        }
    }

    public float GetFollowPercentage()
    {
        return settings.cameraSpeed;
    }

    public void AddEncapsolateObject(Transform obj)
    {
        if (encapsolatedTarget.Contains(obj))
        {
            return;
        }

        this.encapsolatedTarget.Add(obj);
    }

    private Vector3 GetCenterPosition(List<Transform> listOfTargets)
    {
        var bounds = new Bounds(listOfTargets[0].position, Vector3.zero);
        foreach (var target in listOfTargets)
        {
            bounds.Encapsulate(target.position);
        }
        return bounds.center;
    }

    public void SetPosition(Vector3 landingPosition)
    {
        var pos = landingPosition;
        pos.z = host.position.z;
        host.position = pos;
    }
}
