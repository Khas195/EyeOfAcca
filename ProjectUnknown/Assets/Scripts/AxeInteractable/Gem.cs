using System;
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

    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    SpriteRenderer render = null;

    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    Sprite lockedSprite = null;


    [BoxGroup("Optional")]
    [SerializeField]
    GameObject gemParticles = null;


    private void Start()
    {
        if (settings.GemUnlocked == false)
        {
            LockGem();
        }
        settings.OnGemUnlocked.AddListener(ActivateGem);
    }

    private void LockGem()
    {
        this.anim.enabled = false;
        render.sprite = lockedSprite;
        if (gemParticles != null)
        {
            gemParticles.SetActive(false);
        }
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
        if (this.anim.enabled == false)
        {
            VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.CharacterStoneBreaks, this.transform.position, Quaternion.identity);
            this.anim.enabled = true;
        }
        anim.SetBool("HasPower", true);
        if (gemParticles != null)
        {
            gemParticles.SetActive(true);
        }
    }
    public void DeactiveGem()
    {
        anim.SetBool("HasPower", false);
        if (gemParticles != null)
        {
            gemParticles.SetActive(false);
        }
    }
}
