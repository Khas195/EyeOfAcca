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
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    GameMasterSettings settings = null;

    [BoxGroup("Optional")]
    [SerializeField]
    GameObject gemParticles = null;


    private void Start()
    {
        if (settings.GemUnlocked == false)
        {
            DeactiveGem();
        }
        settings.OnGemUnlocked.AddListener(ActivateGem);
    }


    public override void OnAxeHit(Boomeraxe axe)
    {
        if (settings.GemUnlocked == false) return;

        base.OnAxeHit(axe);
        if (axe.SetActiveAbility(ability))
        {
            LogHelper.GetInstance().Log(("Absorbed Power - Teleportation!").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
            DeactiveGem();
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
        if (settings.GemUnlocked == false) return;

        base.OnAxeAbilityTriggered(triggeredAbility);
        LogHelper.GetInstance().Log(("Gem recharging power - Teleportation!").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
        VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.AxeHasPowerFlash, this.transform.position, Quaternion.identity);
        ActivateGem();
        if (gemParticles != null)
        {
            gemParticles.SetActive(true);
        }
    }
    public void ActivateGem()
    {
        anim.SetBool("HasPower", true);
    }
    public void DeactiveGem()
    {
        anim.SetBool("HasPower", false);
    }
}
