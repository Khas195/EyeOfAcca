using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "MovementData", menuName = "Data/GameMasterSettings", order = 1)]
[Serializable]
public class GameMasterSettings : ScriptableObject
{
    public bool skipMainMenu = false;


    [Button("Save")]
    public void SaveData()
    {
        SaveLoadManager.Save(this, this.name);
    }
    [Button("Load")]
    public void LoadData()
    {
        SaveLoadManager.Load(this, this.name);
    }
}
