using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recall", menuName = "AxeBehavior/Recall", order = 1)]
public class RecallAbility : AxeAbility
{
    public override void Activate(Boomeraxe axe)
    {
        LogHelper.GetInstance().Log(("CRACKED!!").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
        axe.Recall();
        LogHelper.GetInstance().Log(("COME TO ME!!!").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
    }

    public override string GetAbilityPower()
    {
        return "hasRecallPower";
    }

    public override Color GetGemColor()
    {
        return Color.cyan;
    }
}
