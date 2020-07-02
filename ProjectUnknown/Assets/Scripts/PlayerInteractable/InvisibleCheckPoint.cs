using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

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
    void Start()
    {
        if (GameMaster.GetInstance().GetCurrentLevelSettings().startLevelDoor.Equals(door))
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
            LogHelper.GetInstance().Log("Check Point Reached".Bolden().Colorize(Color.cyan), true, LogHelper.LogLayer.PlayerFriendly);
            GameMaster.GetInstance().UpdateCurrentLevelSettings(door.GetProfile());
            SaveLoadManager.SaveAllData();
            this.render.sprite = activated;
            VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.AxeHasPowerFlash, flashSpawnPoint.position, Quaternion.identity);
        }
    }
}
