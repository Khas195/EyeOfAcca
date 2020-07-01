using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemActivator : SavePoint
{
    [SerializeField]
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
    private Sprite activatedSprite;
    [SerializeField]
    private Sprite deactivatedSprite;
    [SerializeField]
    private Transform flashSpawnPoint;
    void Start()
    {
        if (settings.GemUnlocked)
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
        base.OnSavePointActivated();

        if (settings.GemUnlocked == false)
        {
            var gatherPower = VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.OrbGatherPower, flashSpawnPoint.transform.position, Quaternion.identity);
            var destroyCallback = gatherPower.GetComponent<DestroyWhenAnimationDone>();
            var gatherPowerAnim = gatherPower.GetComponent<Animator>();
            gatherPowerAnim.SetBool("hasRecallPower", true);
            destroyCallback.OnAnimationDone.AddListener(OnGatherPowerAnimationDone);
        }
    }

    private void OnGatherPowerAnimationDone()
    {
        settings.UnlockGem();
        this.render.sprite = this.activatedSprite;
        settings.SaveData();
    }
}
