using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpawnFlashOnDestroy : MonoBehaviour
{
    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        var effect = VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.AxeHasPowerFlash, Vector3.zero, Quaternion.identity);
        effect.transform.parent = this.transform.parent;
        effect.transform.position = this.transform.position;
    }
}
