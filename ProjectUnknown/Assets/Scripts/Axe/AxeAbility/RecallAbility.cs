using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recall", menuName = "AxeBehavior/Recall", order = 1)]
public class RecallAbility : AxeAbility
{
    public override void Activate(Boomeraxe axe)
    {
        base.Activate(axe);
        LogHelper.GetInstance().Log(("CRACKED!!").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
        axe.Recall();
        LogHelper.GetInstance().Log(("COME TO ME!!!").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
    }
}
