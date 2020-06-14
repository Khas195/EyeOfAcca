using System;
using NaughtyAttributes;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "LevelSettings", menuName = "Data/LevelSettings", order = 1)]
public class LevelSettings : ScriptableObject
{
    [SerializeField]
    public TransitionDoorProfile startSpawn = null;
    [SerializeField]
    public TransitionDoorProfile currentSpawn = null;
    [ReadOnly]
    public string currentLevel = "";

    [ReadOnly]
    public Vector3 levelCenter = Vector3.zero;
    [ReadOnly]
    public Vector3 levelBounds = Vector3.zero;

    public void Reset()
    {
        startSpawn = null;
        currentSpawn = null;
        currentLevel = "";
        levelCenter = Vector3.zero;
        levelBounds = Vector3.zero;
    }
    public void SaveDoorAsStartSpawn(TransitionDoorProfile doorToSave)
    {
        this.startSpawn = doorToSave;
    }
    public bool IsSameStartSpawn(TransitionDoorProfile doorToCompare)
    {
        return startSpawn == doorToCompare;
    }
    public bool DoesStartDoorExist()
    {
        return startSpawn != null;
    }
    [Button]
    public void ClearSave()
    {
        Reset();
        SaveLoadManager.SaveAllData();
    }
}
