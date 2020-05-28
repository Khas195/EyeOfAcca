using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomeraxeVFXHandler : MonoBehaviour
{
    [SerializeField]
    Transform gemPosition = null;
    [SerializeField]
    Boomeraxe axe = null;

    public void SpawnPebbles(Vector3 pos, Quaternion rotation)
    {
        var effect = VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.Pebbles, pos, rotation);
    }
    public void OnAxeStuckWithAbility(Vector3 pos, Quaternion rotation)
    {
        var ability = axe.GetCurrentAbility();
        var effect = VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.AxeHasPowerFlash, Vector2.one, Quaternion.identity);
        effect.transform.position = gemPosition.position;
        effect.transform.parent = axe.GetAxeTransform();
        effect.GetComponent<Animator>().SetBool(ability.GetAbilityPower(), true);
    }
}
