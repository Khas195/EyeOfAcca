using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpawnFlashOnDestroy : MonoBehaviour
{
    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    public void SpawnFlash()
    {
        var effect = VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.AxeHasPowerFlash, this.transform.position, Quaternion.identity);
    }
}
