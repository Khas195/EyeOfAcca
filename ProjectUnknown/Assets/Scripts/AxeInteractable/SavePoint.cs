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

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        if (savedSettings.startSpawn == linkedDoor.GetProfile())
        {
            var lightValue = colorCurve.GetTransitionInKey(1).value;
            var color = spriteRenderer.color;
            color = new Color(lightValue, lightValue, lightValue, 1);
            spriteRenderer.color = color;
            activate = true;
        }
    }
    public override void OnAxeAbilityTriggered(AxeAbility triggeredAbility)
    {
        base.OnAxeAbilityTriggered(triggeredAbility);
    }

    public override void OnAxeHit(Boomeraxe axe)
    {
        base.OnAxeHit(axe);
        savedSettings.startSpawn = linkedDoor.GetProfile();
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

    protected override void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        base.OnCollisionEnter2D(collisionInfo);
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
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
