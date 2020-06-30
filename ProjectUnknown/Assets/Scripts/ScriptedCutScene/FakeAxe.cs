using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeAxe : MonoBehaviour
{
    [SerializeField]
    Transform particleSpawnPoint = null;
    [SerializeField]
    Animator anim = null;


    public void GatherPower()
    {
        var gatherPower = VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.OrbGatherPower, particleSpawnPoint.position, Quaternion.identity);
        var powerAnim = gatherPower.GetComponent<Animator>();
        powerAnim.SetBool("hasRecallPower", true);
        powerAnim.GetComponent<DestroyWhenAnimationDone>().OnAnimationDone.AddListener(this.OnPowerGathered);
    }

    public void OnPowerGathered()
    {
        anim.SetTrigger("Charge");
        VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.AxeHasPowerFlash, particleSpawnPoint.position, Quaternion.identity);
    }
}
