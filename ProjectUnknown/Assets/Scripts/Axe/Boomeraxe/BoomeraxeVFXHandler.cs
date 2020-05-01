using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomeraxeVFXHandler : MonoBehaviour
{

    public void OnTeleport(Vector3 newPos, Vector3 oldPos)
    {
        VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.Teleport, newPos, Quaternion.identity);
        VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.Teleport, oldPos, Quaternion.identity);
    }
    public void OnBounce(Vector3 pos, Quaternion rotation)
    {
        VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.Bounce, pos, rotation);
    }
}
