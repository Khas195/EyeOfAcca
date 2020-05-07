using System;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "Shake Data", menuName = "Data/Shake", order = 1)]
[Serializable]
public class ShakeData : ScriptableObject
{
    [InfoBox("The speed of the shake.", EInfoBoxType.Normal)]
    public float frequency = 25;
    public Vector3 maximumTranslateShake = Vector3.one * 0.5f;

    public Vector3 maximumAngularShake = Vector3.one * 2;

    public float recoverySpeed = 1.5f;
    public float traumaExponent = 2;
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
