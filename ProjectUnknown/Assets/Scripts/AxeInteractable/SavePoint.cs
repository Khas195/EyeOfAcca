using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class SavePoint : AxeInteractable
{
    [SerializeField]
    [Required]
    LevelSettings savedSettings = null;
    [SerializeField]
    [Required]
    LevelTransitionDoor linkedDoor = null;
    [SerializeField]
    [Required]
    SpriteRenderer spriteRenderer = null;
    [SerializeField]
    [Required]
    TransitionCurve colorCurve = null;
    bool activate = false;

    void Start()
    {
        if (savedSettings.IsSameStartSpawn(linkedDoor.GetProfile()))
        {
            var lightValue = colorCurve.GetTransitionInKey(1).value;
            var color = spriteRenderer.color;
            color = new Color(lightValue, lightValue, lightValue, 1);
            spriteRenderer.color = color;
            activate = true;
        }
    }

    public override void OnAxeHit(Boomeraxe axe)
    {
        base.OnAxeHit(axe);
        this.savedSettings.SaveDoorAsStartSpawn(linkedDoor.GetProfile());
        if (activate)
        {
            colorCurve.TransitionIn(colorCurve.GetTransitionInKey(1).time);
        }
        else
        {
            colorCurve.TransitionIn();
        }
        activate = true;
        SaveLoadManager.SaveAllData();
    }

    void Update()
    {
        if (activate)
        {
            if (colorCurve.IsCurrentTimeInGraph() == false) return;

            colorCurve.AdvanceTime(Time.deltaTime);
            var lightValue = colorCurve.GetCurrentValue();
            var color = spriteRenderer.color;
            color = new Color(lightValue, lightValue, lightValue, 1);
            spriteRenderer.color = color;
        }

    }
}
