using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class OnCheckPointReached : UnityEvent<TransitionDoorProfile> { }
public class InvisibleCheckPoint : MonoBehaviour
{
    [SerializeField]
    [Required]
    LevelTransitionDoor door = null;
    [SerializeField]
    [Required]
    SpriteRenderer render = null;
    [SerializeField]
    [Required]
    Sprite activated = null;
    [SerializeField]
    [Required]
    Sprite normal = null;
    [SerializeField]
    [Required]
    Transform flashSpawnPoint = null;
    [SerializeField]
    bool isDoorInvisible = false;
    public static OnCheckPointReached CheckPointReachedEvent = new OnCheckPointReached();
    void Start()
    {
        if (GameMaster.GetInstance().IsCurrentCheckPoint(door.GetProfile()))
        {
            render.sprite = activated;
        }
        else
        {
            render.sprite = normal;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            if (GameMaster.GetInstance().IsCurrentCheckPoint(door.GetProfile()) == false)
            {
                LogHelper.GetInstance().Log("Check Point Reached".Bolden().Colorize(Color.cyan), true, LogHelper.LogLayer.PlayerFriendly);
                CheckPointReachedEvent.Invoke(door.GetProfile());
                SaveLoadManager.SaveAllData();
                this.render.sprite = activated;
                if (isDoorInvisible == false)
                {
                    VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.AxeHasPowerFlash, flashSpawnPoint.position, Quaternion.identity);
                }
            }
        }
    }
}
