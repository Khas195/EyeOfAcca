using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField]
    [Required]
    AxeAbility ability = null;
    [SerializeField]
    [Required]
    Collider2D col2d = null;
    [SerializeField]
    [Required]
    Animator anim = null;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Boomeraxe"))
        {
            if (other.GetComponent<BoomeraxeCollisionDetection>().GetAxe().SetActiveAbility(ability, OnAbilityUsed))
            {
                LogHelper.GetInstance().Log(("Absorbed Power - Teleportation!").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
                anim.SetBool("HasPower", false);
            }
        }
    }
    public void OnAbilityUsed(AxeAbility usedAbility)
    {
        if (usedAbility.Equals(usedAbility))
        {
            LogHelper.GetInstance().Log(("Gem recharging power - Teleportation!").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
            anim.SetBool("HasPower", true);
        }
    }
}
