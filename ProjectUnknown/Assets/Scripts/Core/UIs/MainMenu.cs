using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    LevelSettings savedLevelData = null;
    [SerializeField]
    LevelSettings startLevelData = null;
    [SerializeField]
    GameMasterSettings settings = null;
    [SerializeField]
    GameObject continueBtn = null;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        if (savedLevelData.startLevelDoor != null && savedLevelData.startLevelDoor != startLevelData.startLevelDoor)
        {
            continueBtn.SetActive(true);
        }
        else
        {
            continueBtn.SetActive(false);
        }
    }
    public void Continue()
    {
        GameMaster.GetInstance().LoadLevelAtSpawn(savedLevelData.startLevelDoor);
    }
    public void Exit()
    {
        GameMaster.GetInstance().ExitGame();
    }
    public void LoadFistLevel()
    {
        GameMaster.GetInstance().StartNewGame();
    }
    public void SwitchScreenMode()
    {
        settings.mode = settings.mode == FullScreenMode.ExclusiveFullScreen ? FullScreenMode.Windowed : FullScreenMode.ExclusiveFullScreen;
        if (Screen.fullScreenMode != settings.mode)
        {
            Screen.fullScreenMode = settings.mode;
        }
    }
}
