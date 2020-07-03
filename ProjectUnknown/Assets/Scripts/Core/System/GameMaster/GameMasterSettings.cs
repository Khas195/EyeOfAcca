using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "MovementData", menuName = "Data/GameMasterSettings", order = 1)]
[Serializable]
public class GameMasterSettings : ScriptableObject, ISaveRestable
{
    public FullScreenMode mode = FullScreenMode.ExclusiveFullScreen;
    public bool skipMainMenu = false;
    [SerializeField]
    private bool gemUnlocked = false;
    [SerializeField]
    private bool railUnlocked = false;
    [SerializeField]
    private bool timedDoorUnlocked = false;
    [SerializeField]
    public bool isNewGame = true;
    [SerializeField]
    public bool openingCutSceneDone = false;

    public UnityEvent OnGemUnlocked = new UnityEvent();
    public UnityEvent OnRailUnlocked = new UnityEvent();
    public UnityEvent OnTimedDoorUnlock = new UnityEvent();

    public bool GemUnlocked { get => gemUnlocked; }
    public bool RailUnlocked { get => railUnlocked; }
    public bool TimedDoorUnlock { get => timedDoorUnlocked; }

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

    public void Reset()
    {
        LockGem();
        LockRail();
        LockTimedDoor();
        openingCutSceneDone = false;
        isNewGame = true;
    }
    public void UnlockGem()
    {
        gemUnlocked = true;
        OnGemUnlocked.Invoke();
    }
    public void LockGem()
    {
        gemUnlocked = false;
    }
    public void UnlockRail()
    {
        railUnlocked = true;
        OnRailUnlocked.Invoke();
    }
    public void LockRail()
    {
        railUnlocked = false;
    }
    public void UnlockTimedDoor()
    {
        timedDoorUnlocked = true;
        OnTimedDoorUnlock.Invoke();
    }
    public void LockTimedDoor()
    {
        timedDoorUnlocked = false;
    }

    public void ResetSave()
    {
        LogHelper.GetInstance().Log(("Reset Game Master Settings").Bolden(), true);
        this.Reset();
    }
}
