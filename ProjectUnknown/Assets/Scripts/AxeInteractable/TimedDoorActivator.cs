using NaughtyAttributes;
using UnityEngine;

public class TimedDoorActivator : SavePoint
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
        if (settings.TimedDoorUnlock)
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
        settings.UnlockTimedDoor();
        render.color = activatedColor;
        settings.SaveData();
    }
}
