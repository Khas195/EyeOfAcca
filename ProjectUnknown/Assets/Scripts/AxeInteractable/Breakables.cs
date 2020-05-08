using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : AxeInteractable
{
    public override void OnAxeAbilityTriggered(AxeAbility triggeredAbility)
    {
        base.OnAxeAbilityTriggered(triggeredAbility);
    }

    public override void OnAxeHit(Boomeraxe axe)
    {
        base.OnAxeHit(axe);
        LogHelper.GetInstance().Log(("BREAK POT").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
    }


}
