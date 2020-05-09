using System;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "Shake Data", menuName = "Data/Shake", order = 1)]
[Serializable]
public class ShakeData : ScriptableObject
{
    public float shakeDuration = 0.5f;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

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
