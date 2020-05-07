using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Animator animator = null;
    [SerializeField]
    ChooseLevelManager chooseLevelManager = null;
    public void GoToMainMenu()
    {
        animator.SetTrigger("TriggerMainMenu");
    }
    public void GoToChooseLevel()
    {
        chooseLevelManager.RefreshLevelView();
        animator.SetTrigger("TriggerChooseLevel");
    }
    public void Exit()
    {
        GameMaster.GetInstance().ExitGame();
    }
    public void LoadFistLevel()
    {
        GameMaster.GetInstance().LoadLevel(GameMaster.GetInstance().GetStartLevel());
    }
}
