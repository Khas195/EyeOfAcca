using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Breakables : AxeInteractable
{
    [SerializeField]
    [Required]
    SpriteRenderer spriteRender = null;
    [SerializeField]
    [Required]
    Sprite brokenState = null;
    [SerializeField]
    [Required]
    Collider2D box = null;
    [SerializeField]
    UnityEvent onPotBreak = new UnityEvent();


    public override void OnAxeAbilityTriggered(AxeAbility triggeredAbility)
    {
        base.OnAxeAbilityTriggered(triggeredAbility);
    }

    public override void OnAxeHit(Boomeraxe axe)
    {
        base.OnAxeHit(axe);
        LogHelper.GetInstance().Log(("BREAK POT").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
        spriteRender.sprite = brokenState;
        box.enabled = false;
        VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.PotBreak, this.transform.position, Quaternion.identity);
        onPotBreak.Invoke();
    }


}
