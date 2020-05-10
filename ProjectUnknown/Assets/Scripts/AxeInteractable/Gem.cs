﻿using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Gem : AxeInteractable
{
    [SerializeField]
    [Required]
    AxeAbility ability = null;
    [SerializeField]
    [Required]
    Animator anim = null;



    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
    }
    public override void OnAxeHit(Boomeraxe axe)
    {
        base.OnAxeHit(axe);
        if (axe.SetActiveAbility(ability))
        {
            LogHelper.GetInstance().Log(("Absorbed Power - Teleportation!").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
            anim.SetBool("HasPower", false);
            VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.OnTeleGemHit, this.transform.position, Quaternion.identity);
        }
    }

    public override void OnAxeAbilityTriggered(AxeAbility triggeredAbility)
    {
        base.OnAxeAbilityTriggered(triggeredAbility);
        LogHelper.GetInstance().Log(("Gem recharging power - Teleportation!").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
        VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.AxeHasPowerFlash, this.transform.position, Quaternion.identity);
        anim.SetBool("HasPower", true);
    }
}
