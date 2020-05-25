using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraSettings", menuName = "Data/CameraSettings", order = 1)]
public class CameraSettings : ScriptableObject
{
    public float cameraSpeed = 0.08f;

    public Vector2 cameraFollowDeadZoneBoxSize = Vector2.one;

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
