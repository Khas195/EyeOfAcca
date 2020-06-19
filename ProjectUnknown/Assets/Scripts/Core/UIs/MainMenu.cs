using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
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
        if (GameMaster.GetInstance().HasSave())
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
        GameMaster.GetInstance().ContinueSaved();
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
