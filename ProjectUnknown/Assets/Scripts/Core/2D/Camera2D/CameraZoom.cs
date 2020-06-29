using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField]
    Camera host = null;
    [SerializeField]
    CameraSettings settings = null;
    [SerializeField]
    List<Transform> encapsulatedTargets = new List<Transform>();
    [SerializeField]
    private Transform character = null;

    [SerializeField]
    [Tooltip("Each frame the camera move x percentage closer to the target")]
    float followPercentage;

    public bool IsNormalZoom()
    {
        return Mathf.Abs(host.orthographicSize - settings.minZoom) < 0.1f;
    }
    public void RemoveEncapsulateObject(Transform target)
    {
        if (encapsulatedTargets.Contains(target))
        {
            encapsulatedTargets.Remove(target);
        }
    }

    // Update is called once per frame
    public void UpdateZoom()
    {
        var greatestDistance = GetGreatestDistance(encapsulatedTargets);
        var clampedZoomValue = Mathf.Clamp(greatestDistance, settings.minZoom, settings.maxZoom);
        host.orthographicSize = Mathf.Lerp(host.orthographicSize, clampedZoomValue, settings.cameraZoomSpeed * Time.deltaTime);
    }

    public void Clear(bool clearPlayer)
    {
        encapsulatedTargets.Clear();
        if (clearPlayer == false)
        {
            encapsulatedTargets.Add(character);
        }
    }

    public float GetMaxZoom()
    {
        return settings.maxZoom;
    }

    public void SetMaxZoom(float newMaxZoom)
    {
        this.settings.maxZoom = newMaxZoom;
    }

    public void AddEncapsolateObject(Transform obj)
    {
        if (encapsulatedTargets.Contains(obj))
        {
            return;
        }
        encapsulatedTargets.Add(obj);
    }

    public float GetFollowPercentage()
    {
        return followPercentage;
    }


    float GetGreatestDistance(List<Transform> encapsulatedTargets)
    {
        if (encapsulatedTargets.Count <= 1)
        {
            return 0;
        }
        var bounds = new Bounds(encapsulatedTargets[0].transform.position, Vector3.one);
        for (int i = 1; i < encapsulatedTargets.Count; i++)
        {
            bounds.Encapsulate(encapsulatedTargets[i].transform.position);
        }
        var result = bounds.size.x < bounds.size.y ? bounds.size.y : bounds.size.x;
        return result;
    }

    public void SetFollowPercentage(float zoomFollowPercentage)
    {
        followPercentage = zoomFollowPercentage;
    }
}
