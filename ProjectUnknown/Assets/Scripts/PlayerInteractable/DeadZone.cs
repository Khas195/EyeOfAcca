using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : AxeInteractable
{
    public override void OnAxeAbilityTriggered(AxeAbility triggeredAbility)
    {
        base.OnAxeAbilityTriggered(triggeredAbility);
    }

    public override void OnAxeHit(Boomeraxe axe)
    {
        base.OnAxeHit(axe);
        VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.Pebbles, axe.GetAxePosition(), Quaternion.identity);
        axe.NullAbility();
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.tag.Equals("Player"))
        {
            SFXSystem.GetInstance().StopAllSounds();
            LogHelper.GetInstance().Log(("Player entered Dead Zone").Bolden(), true, LogHelper.LogLayer.PlayerFriendly);
            GameMaster.GetInstance().LoadLevel(GameMaster.GetInstance().GetStartLevel());
        }
    }
}
