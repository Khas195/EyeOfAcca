using System;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "Shake Data", menuName = "Data/Shake", order = 1)]
[Serializable]
public class ShakeData : ScriptableObject
{
    [NonSerialized]
    public float shakeDuration = 0.5f;
    [Tooltip("Shake amount = trauma * trauma")]
    [ReadOnly]
    public float shakeAmount = 0.7f;

    [Range(0.0f, 1.0f)]
    public float trauma = 0.2f;
    [Tooltip("How fast trauma is reduced. Current Trauma = Time.deltaTime * decreaseFactor")]
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
