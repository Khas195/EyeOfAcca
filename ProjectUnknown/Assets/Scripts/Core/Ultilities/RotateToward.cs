using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class RotateToward : MonoBehaviour
{

    [SerializeField]
    bool manualAssign = false;

    [SerializeField]
    [ShowIf("manualAssign")]
    Camera playerCam = null;

    [SerializeField]
    [ShowIf("manualAssign")]
    GameObject pivot = null;
    public void RotateXAxisTowardMouse()
    {
        Vector3 mousPos = playerCam.ScreenToWorldPoint(Input.mousePosition);
        RotateXAxisToward(mousPos);
    }

    public void RotateXAxisToward(Vector3 position)
    {
        Vector3 perpendicular = Vector3.Cross(pivot.transform.position - position, Vector3.forward);
        pivot.transform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);
    }
    public void SetPivot(GameObject pivot)
    {
        this.pivot = pivot;
    }
    public void SetCamera(Camera cam)
    {
        playerCam = cam;
    }
}
