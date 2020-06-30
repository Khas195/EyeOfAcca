using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeAxe : MonoBehaviour
{
    [SerializeField]
    Transform particleSpawnPoint = null;
    [SerializeField]
    Animator anim = null;


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.AxeHasPowerFlash, particleSpawnPoint.position, Quaternion.identity);
            anim.SetTrigger("Charge");
        }
    }
}
