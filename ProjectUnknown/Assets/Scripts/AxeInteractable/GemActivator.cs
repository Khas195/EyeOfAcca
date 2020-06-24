using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemActivator : SavePoint
{
    [SerializeField]
    GameMasterSettings settings = null;
    [SerializeField]
    SpriteRenderer render = null;
    [SerializeField]
    Color activatedColor = Color.white;
    [SerializeField]
    Color deactivatedColor = Color.black;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        if (settings.GemUnlocked)
        {
            render.color = activatedColor;
        }
        else
        {
            render.color = deactivatedColor;
        }
    }
    public override void OnSavePointActivated()
    {
        base.OnSavePointActivated();
        settings.UnlockGem();
        render.color = activatedColor;
        settings.SaveData();
    }

}
