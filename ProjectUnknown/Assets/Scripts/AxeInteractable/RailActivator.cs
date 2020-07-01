using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
public class RailActivator : SavePoint
{
    [SerializeField]
    [Required]
    GameMasterSettings settings = null;
    [SerializeField]
    SpriteRenderer render = null;
    /*
    [SerializeField]
    Color activatedColor = Color.white;
    [SerializeField]
    Color deactivatedColor = Color.black;
    */
    [SerializeField]
    private Sprite deactivatedSprite;
    [SerializeField]
    private Sprite activatedSprite;
    [SerializeField]
    private Transform flashSpawnPoint;
    void Start()
    {
        if (settings.RailUnlocked)
        {
            this.render.sprite = this.activatedSprite;
        }
        else
        {
            this.render.sprite = this.deactivatedSprite;
        }
    }
    public override void OnSavePointActivated()
    {
        if (settings.RailUnlocked == false)
        {
            var gatherPower = VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.OrbGatherPower, flashSpawnPoint.transform.position, Quaternion.identity);
            var destroyCallback = gatherPower.GetComponent<DestroyWhenAnimationDone>();
            var gatherPowerAnim = gatherPower.GetComponent<Animator>();
            gatherPowerAnim.SetBool("hasRecallPower", true);
            destroyCallback.OnAnimationDone.AddListener(OnGatherPowerAnimationDone);
        }
        base.OnSavePointActivated();
    }

    private void OnGatherPowerAnimationDone()
    {
        var flash = VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.AxeHasPowerFlash, flashSpawnPoint.transform.position, Quaternion.identity);
        this.render.sprite = this.activatedSprite;
        settings.UnlockRail();
        settings.SaveData();
    }
}
