using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class DoorMechanicUnlock : MonoBehaviour
{
    [SerializeField]
    [Required]
    SpriteRenderer render = null;
    [SerializeField]
    [Required]
    Sprite normalDoor = null;
    [SerializeField]
    [Required]
    Sprite unlockedDoor = null;
    [SerializeField]
    [Required]
    GameMasterSettings settings = null;
    [SerializeField]
    bool checkGem = false;
    [SerializeField]
    bool checkRail = false;
    [SerializeField]
    bool checkLevelDoor = false;
    void Start()
    {
        UpdateDoorSprite();
        if (checkGem)
        {
            settings.OnGemUnlocked.AddListener(UpdateDoorSprite);
        }
        if (checkRail)
        {
            settings.OnRailUnlocked.AddListener(UpdateDoorSprite);
        }
        if (checkLevelDoor)
        {
            settings.OnTimedDoorUnlock.AddListener(UpdateDoorSprite);
        }
    }

    private void UpdateDoorSprite()
    {
        if (checkGem && settings.GemUnlocked == false || checkRail && settings.RailUnlocked == false || checkLevelDoor && settings.TimedDoorUnlock == false)
        {
            render.sprite = normalDoor;
        }
        else
        {
            render.sprite = unlockedDoor;
        }
    }
}
