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
        Vector3 mousPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        mousPos = playerCam.ScreenToWorldPoint(mousPos);
        mousPos.z = 0;
        RotateXAxisToward(mousPos);
    }

    public void RotateXAxisToward(Vector3 position, bool setRot = false)
    {
        var pivotPos = pivot.transform.position;
        pivotPos.z = 0;
        position.z = 0;
        Vector3 perpendicular = Vector3.Cross(pivotPos - position, Vector3.forward);
        Quaternion lookRot = Quaternion.LookRotation(Vector3.forward, perpendicular);
        if (setRot == true)
        {
            pivot.transform.rotation = lookRot;
        }
        else
        {
            pivot.transform.rotation = Quaternion.Lerp(pivot.transform.rotation, lookRot, 20f * Time.deltaTime);
        }
    }
    public void RotateXAxisTowardDir(Vector3 normalizedDir, bool setRot = false)
    {
        Vector3 perpendicular = Vector3.Cross(normalizedDir, Vector3.forward);
        Quaternion lookRot = Quaternion.LookRotation(Vector3.forward, perpendicular);
        if (setRot == true)
        {
            pivot.transform.rotation = lookRot;
        }
        else
        {

            pivot.transform.rotation = Quaternion.Lerp(pivot.transform.rotation, lookRot, 20f * Time.deltaTime);
        }
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
