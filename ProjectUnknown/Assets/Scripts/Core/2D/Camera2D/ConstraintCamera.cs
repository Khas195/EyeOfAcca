using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstraintCamera : MonoBehaviour
{
    [SerializeField]
    Transform host = null;
    [SerializeField]
    Vector2 boundBoxSize = Vector2.one;
    [SerializeField]
    CameraFollow follow = null;

    [SerializeField]
    Camera cam = null;
    // Start is called before the first frame update
    void Start()
    {

    }
    void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;
        var hostPos = host.position;

        var cameraSizeY = 2 * cam.orthographicSize;
        var cameraSizeX = cameraSizeY * cam.aspect;
        Gizmos.DrawWireCube(hostPos, new Vector3(cameraSizeX, cameraSizeY, 0));
        Gizmos.DrawWireCube(this.transform.position, boundBoxSize);
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
        bool cameraNeedToAdjust = false;
        var rightBound = this.transform.position.x + this.boundBoxSize.x / 2;
        var leftBound = this.transform.position.x - this.boundBoxSize.x / 2;
        if (hostPos.x + cameraSizeX / 2 >= rightBound)
        {
            hostPos.x = rightBound - cameraSizeX / 2;
            cameraNeedToAdjust = true;
        }
        else if (hostPos.x - cameraSizeX / 2 <= leftBound)
        {
            hostPos.x = leftBound + cameraSizeX / 2;
            cameraNeedToAdjust = true;
        }

        var upperBound = this.transform.position.y + this.boundBoxSize.y / 2;
        var lowerBound = this.transform.position.y - this.boundBoxSize.y / 2;
        if (hostPos.y + cameraSizeY / 2 >= upperBound)
        {
            hostPos.y = upperBound - cameraSizeY / 2;
            cameraNeedToAdjust = true;
        }
        else if (hostPos.y - cameraSizeY / 2 <= lowerBound)
        {
            hostPos.y = lowerBound + cameraSizeY / 2;
            cameraNeedToAdjust = true;
        }


        return hostPos;
    }
}
