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
    bool activate = false;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        if (savedSettings.IsSameStartSpawn(this.linkedDoor.GetProfile()))
        {
            this.ActivateSavePoint();
        }
    }

    public override void OnAxeHit(Boomeraxe axe)
    {
        base.OnAxeHit(axe);
        this.savedSettings.SaveDoorAsStartSpawn(linkedDoor.GetProfile());
        if (activate == false)
        {
            this.ActivateSavePoint();
        }
    }
    public void ActivateSavePoint()
    {
        this.activate = true;
        this.OnSavePointActivated();
    }
    public void DeactivateSavePoint()
    {
        this.activate = false;
        this.OnSavePointDeactivated();
    }
    public virtual void OnSavePointActivated()
    {
        this.savedSettings.SaveData();
    }
    public virtual void OnSavePointDeactivated()
    {
        this.savedSettings.SaveData();
    }
}
