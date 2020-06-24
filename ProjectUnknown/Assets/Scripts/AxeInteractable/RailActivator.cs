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
    [SerializeField]
    Color activatedColor = Color.white;
    [SerializeField]
    Color deactivatedColor = Color.black;
    void Start()
    {
        if (settings.RailUnlocked)
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
        settings.UnlockRail();
        render.color = activatedColor;
        settings.SaveData();
    }
}
