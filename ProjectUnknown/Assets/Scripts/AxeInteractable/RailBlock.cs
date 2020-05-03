using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class RailBlock : AxeInteractable
{
    [SerializeField]
    [Required]
    AxeAbility abilityToInteract = null;
    [SerializeField]
    [Required]
    BoxCollider2D box = null;
    [SerializeField]
    float timeTillDestinationReached = 1f;
    Transform holderTrans = null;
    public override void OnAxeHit(Boomeraxe axe)
    {
        base.OnAxeHit(axe);
        LogHelper.GetInstance().Log(("Axe hit Block!").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
        holderTrans = axe.GetHolder();
    }

    public override void OnAxeAbilityTriggered(AxeAbility triggeredAbility)
    {
        base.OnAxeAbilityTriggered(triggeredAbility);
        if (abilityToInteract.Equals(triggeredAbility))
        {
            if (holderTrans.position.x >= this.transform.position.x)
            {
                LogHelper.GetInstance().Log(("PULL BLOCK RIGHT!").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
            }
            else
            {
                LogHelper.GetInstance().Log(("PULL BLOCK LEFT!").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
            }
        }
    }
}
