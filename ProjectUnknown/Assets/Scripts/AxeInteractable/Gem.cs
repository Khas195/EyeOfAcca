using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Gem : AxeInteractable
{
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    AxeAbility ability = null;

    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    Animator anim = null;

    [BoxGroup("Optional")]
    [SerializeField]
    GameObject gemParticles = null;



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
            if (gemParticles != null)
            {
                gemParticles.SetActive(false);
            }
            var ripple = Ripple.GetInstance(false);
            if (ripple)
            {
                ripple.RippleAt(this.transform.position.x, this.transform.position.y, 0.9f, 15f);
            }
            Time.timeScale = 0.2f;
            StartCoroutine(ResetTimeScaleAfter(0.009f));

        }
    }
    public IEnumerator ResetTimeScaleAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Time.timeScale = 1.0f;
    }

    public override void OnAxeAbilityTriggered(AxeAbility triggeredAbility)
    {
        base.OnAxeAbilityTriggered(triggeredAbility);
        LogHelper.GetInstance().Log(("Gem recharging power - Teleportation!").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
        VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.AxeHasPowerFlash, this.transform.position, Quaternion.identity);
        anim.SetBool("HasPower", true);
        if (gemParticles != null)
        {
            gemParticles.SetActive(true);
        }
    }
}
