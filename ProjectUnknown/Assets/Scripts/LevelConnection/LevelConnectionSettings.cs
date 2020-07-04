using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class DoorConnection
{
    [SerializeField]
    [ReadOnly]
    TransitionDoorProfile HomeDoor = null;

    [SerializeField]
    [Dropdown("AvailableDoors")]
    TransitionDoorProfile LandingDoor = null;
    public DoorConnection(ref TransitionDoorProfile home, ref TransitionDoorProfile land)
    {
        this.HomeDoor = home;
        this.LandingDoor = land;
    }
    public void OnLandingDoorChanged()
    {
        HomeDoor.SetLandPlace(LandingDoor);
        if (LandingDoor != null)
        {
            LandingDoor.SetLandPlace(HomeDoor);
        }
    }
    public DropdownList<TransitionDoorProfile> AvailableDoors()
    {
        var list = new DropdownList<TransitionDoorProfile>();
        var profileList = Resources.LoadAll("GameSettings/LevelDoor", typeof(ScriptableObject));
        foreach (var item in profileList)
        {
            var profile = (TransitionDoorProfile)item;
            if (item.name.Contains("Invisible")) continue;
            list.Add(profile.name, profile);
        }
        return list;
    }
}
[Serializable]
public class LevelDoorsSettings
{
    [OnValueChanged("OnSceneSet")]
    [Scene]
    public String sceneName;
    public List<DoorConnection> doors = new List<DoorConnection>();
    public void OnSceneSet()
    {
        var profileList = Resources.LoadAll("GameSettings/LevelDoor/" + sceneName, typeof(ScriptableObject));
        doors.Clear();
        foreach (var item in profileList)
        {
            var profile = (TransitionDoorProfile)item;
            if (item.name.Contains("Invisible")) continue;
            doors.Add(new DoorConnection(ref profile, ref profile.landingPlace));
        }
    }
    public void OnDoorChanged()
    {
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].OnLandingDoorChanged();
        }
    }
}
[CreateAssetMenu(fileName = "LevelConnections", menuName = "Data/LevelConnection", order = 1)]
public class LevelConnectionSettings : ScriptableObject
{
    [ReorderableList]
    public List<LevelDoorsSettings> settings = new List<LevelDoorsSettings>();
    [Button]
    public void RefreshConnection()
    {
        for (int i = 0; i < settings.Count; i++)
        {
            settings[i].OnSceneSet();
        }
    }
    [Button]
    public void SAVE()
    {
        for (int i = settings.Count - 1; i >= 0; i--)
        {
            settings[i].OnDoorChanged();
        }
    }
}
