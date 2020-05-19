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
    CameraSettings settings;
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


    // Start is called before the first frame update
    void Start()
    {
        encapsolatedTarget.Add(character);
        if (GameMaster.GetInstance().SpawnPositionSet() == true)
        {
            var pos = GameMaster.GetInstance().GetSpawnPosition();
            pos.z = host.position.z;
            host.position = pos;
        }
    }

    void FixedUpdate()
    {
        var targetPos = GetCenterPosition(encapsolatedTarget);
        var hostPos = host.position;
        if (followX)
        {
            hostPos.x = Mathf.Lerp(hostPos.x, targetPos.x, settings.followPercentage);
        }
        if (followY)
        {
            hostPos.y = Mathf.Lerp(hostPos.y, targetPos.y, settings.followPercentage);
        }
        if (followZ)
        {
            hostPos.z = Mathf.Lerp(hostPos.z, targetPos.z, settings.followPercentage);
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
        settings.followPercentage = value;
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
        return settings.followPercentage;
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

}
