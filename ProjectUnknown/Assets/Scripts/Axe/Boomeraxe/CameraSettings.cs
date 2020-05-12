using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraSettings", menuName = "Data/CameraSettings", order = 1)]
public class CameraSettings : ScriptableObject
{
    [Range(0, 1f)]
    public float followPercentage = 0.08f;
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
