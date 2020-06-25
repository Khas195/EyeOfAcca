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
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        if (settings.GemUnlocked)
        {
            //render.color = activatedColor;
            this.render.sprite = this.activatedSprite;
        }
        else
        {
            //render.color = deactivatedColor;
            this.render.sprite = this.deactivatedSprite;
        }
    }
    public override void OnSavePointActivated()
    {
        base.OnSavePointActivated();
        settings.UnlockGem();
        //render.color = activatedColor;
        this.render.sprite = this.activatedSprite;
        settings.SaveData();
    }

}
