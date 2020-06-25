using System;
using NaughtyAttributes;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "LevelSettings", menuName = "Data/LevelSettings", order = 1)]
public class LevelSettings : ScriptableObject, ISaveRestable
{
    [SerializeField]
    public TransitionDoorProfile startLevelDoor = null;
    [ReadOnly]
    public string currentLevel = "";

    [ReadOnly]
    public Vector3 levelCenter = Vector3.zero;
    [ReadOnly]
    public Vector3 levelBounds = Vector3.zero;
    [SerializeField]
    public LevelCollectablesData currentCollectableData = null;
    private int deadCount = 0;

    public int DeadCount { get => deadCount; }

    public void Reset()
    {
        startLevelDoor = null;
        currentLevel = "";
        levelCenter = Vector3.zero;
        levelBounds = Vector3.zero;
        deadCount = 0;
    }
    public void IncreaseDeadCount()
    {
        deadCount += 1;
    }
    public void SaveDoorAsStartSpawn(TransitionDoorProfile doorToSave)
    {
        this.startLevelDoor = doorToSave;
    }
    public bool IsSameStartSpawn(TransitionDoorProfile doorToCompare)
    {
        return startLevelDoor == doorToCompare;
    }
    public bool DoesStartDoorExist()
    {
        return startLevelDoor != null;
    }
    [Button]
    public void ClearSave()
    {
        Reset();
        SaveLoadManager.SaveAllData();
    }

    public void ResetSave()
    {

        LogHelper.GetInstance().Log(("Reset Saved Level Data").Bolden(), true);
        this.Reset();
    }
}
