using NaughtyAttributes;
using UnityEngine;

public class TimedDoorActivator : SavePoint
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
    [SerializeField]
    private GameObject flashObject;

    void Start()
    {
        if (settings.TimedDoorUnlock)
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
        settings.UnlockTimedDoor();
        //render.color = activatedColor;
        this.render.sprite = this.activatedSprite;
        Instantiate(this.flashObject, this.flashSpawnPoint);
        settings.SaveData();
    }
}
