using System;
using System.Xml.Serialization;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/Character", order = 1)]
[Serializable]
public class CharacterData : ScriptableObject
{
    [InfoBox("Remember to SAVE datas after adjusting this data object", EInfoBoxType.Warning)]
    public string characterName;
    public int health;

    public int stamina;

    [Button("Save")]
    public void SaveData()
    {
        SaveLoadManager.Save<CharacterData>(this, this.name);
    }
    [Button("Load")]
    public void LoadData()
    {
        SaveLoadManager.Load<CharacterData>(this, this.name);
    }
}
