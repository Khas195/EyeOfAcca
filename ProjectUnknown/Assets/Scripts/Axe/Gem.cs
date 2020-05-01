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
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Boomeraxe"))
        {
            LogHelper.GetInstance().Log(("Absorbed Power - Teleportation!").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
            other.GetComponent<BoomeraxeCollisionDetection>().GetAxe().SetActiveAbility(ability);
        }
    }
}
