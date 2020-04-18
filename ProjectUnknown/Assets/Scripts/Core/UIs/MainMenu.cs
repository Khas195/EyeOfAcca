using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    [Required]
    Animator animator = null;
    [SerializeField]
    [Required]
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
        GameMaster.GetInstance().LoadLevel(1);
    }
}
