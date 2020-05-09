using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomeraxeVFXHandler : MonoBehaviour
{
    [SerializeField]
    Transform gemPosition = null;
    [SerializeField]
    Boomeraxe axe = null;
    public void OnTeleport(Vector3 newPos, Vector3 oldPos)
    {
        VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.Teleport, newPos, Quaternion.identity);
        VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.Teleport, oldPos, Quaternion.identity);
    }
    public void OnAxeStuck(Vector3 pos, Quaternion rotation)
    {
        var effect = VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.AxeHasPowerFlash, Vector3.zero, Quaternion.identity);
        effect.transform.parent = gemPosition;
        effect.transform.localPosition = Vector2.zero;
        effect.transform.localRotation = Quaternion.identity;
    }
    public void OnAxeStuckWithAbility(AxeAbility ability)
    {

    }
}
