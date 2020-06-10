using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstraintCamera : MonoBehaviour
{
    [SerializeField]
    Transform host = null;
    [SerializeField]
    private Vector2 boundPos = Vector2.one;
    [SerializeField]
    Vector2 boundBoxSize = Vector2.one;

    [SerializeField]
    Camera cam = null;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        var master = GameMaster.GetInstance(false);
        if (master)
        {
            var levelSettings = master.GetCurrentLevelSettings();
        }
    }
    void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;
        var hostPos = host.position;

        var cameraSizeY = 2 * cam.orthographicSize;
        var cameraSizeX = cameraSizeY * cam.aspect;
        Gizmos.DrawWireCube(host.position, new Vector3(cameraSizeX, cameraSizeY, 0));
        Gizmos.DrawWireCube(boundPos, boundBoxSize);
    }
    private void LateUpdate()
    {
        var hostPos = host.position;

        var cameraSizeY = 2 * cam.orthographicSize;
        var cameraSizeX = cameraSizeY * cam.aspect;
        hostPos = Constraint(hostPos, cameraSizeY, cameraSizeX);


        host.transform.position = hostPos;

    }
    private Vector3 Constraint(Vector3 hostPos, float cameraSizeY, float cameraSizeX)
    {
        var rightBound = boundPos.x + this.boundBoxSize.x / 2;
        var leftBound = boundPos.x - this.boundBoxSize.x / 2;
        if (hostPos.x + cameraSizeX / 2 >= rightBound)
        {
            hostPos.x = rightBound - cameraSizeX / 2;
        }
        else if (hostPos.x - cameraSizeX / 2 <= leftBound)
        {
            hostPos.x = leftBound + cameraSizeX / 2;
        }

        var upperBound = boundPos.y + this.boundBoxSize.y / 2;
        var lowerBound = boundPos.y - this.boundBoxSize.y / 2;
        if (hostPos.y + cameraSizeY / 2 >= upperBound)
        {
            hostPos.y = upperBound - cameraSizeY / 2;
        }
        else if (hostPos.y - cameraSizeY / 2 <= lowerBound)
        {
            hostPos.y = lowerBound + cameraSizeY / 2;
        }


        return hostPos;
    }

    public void SetConstraint(Vector2 position, Vector2 size)
    {
        this.boundPos = position;
        this.boundBoxSize = size;
    }
}
