using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraSettings", menuName = "Data/CameraSettings", order = 1)]
public class CameraSettings : ScriptableObject
{
    public float cameraSpeed = 0.08f;
    public float cameraZoomSpeed = 3f;
    [Range(0.0f, 1.0f)]
    public float cameraSizePercentage = 0.5f;

    public Vector2 cameraFollowDeadZoneBoxSize = Vector2.one;
    public float leadSpeed = 3f;
    public float leadDistance = 3f;
    public float lookDownDistance = 4f;
    public float lookDownHoldTIme = 1f;
    public float maxZoom = 14f;
    public float minZoom = 10f;

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
