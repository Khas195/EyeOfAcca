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
    GameObject continueBtn = null;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        if (savedLevelData.startSpawn == true)
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
        GameMaster.GetInstance().LoadLevelAtSpawn(savedLevelData.startSpawn);
    }
    public void Exit()
    {
        GameMaster.GetInstance().ExitGame();
    }
    public void LoadFistLevel()
    {
        GameMaster.GetInstance().StartNewGame();
        GameMaster.GetInstance().InitiateLoadLevelSequence(GameMaster.GetInstance().GetStartLevel(), newSave: true);
    }
}
