using System;
using System.Diagnostics;
using UnityEngine;

public class LogHelper : SingletonMonobehavior<LogHelper>
{
    public enum LogLayer
    {
        Console,
        Developer,
        PlayerFriendly
    }
    [SerializeField]
    LogLayer currentMode = LogLayer.PlayerFriendly;

    [Conditional("ENABLE_LOGS")]
    public void LogError(string message, bool showLogOnInGame = false, LogLayer mode = LogLayer.Console)
    {
        UnityEngine.Debug.LogError(message);

        if (showLogOnInGame && Application.isPlaying)
        {
            if (mode >= currentMode)
            {
                ShowLogOnUI("ERROR:".TextMod("red", true, true) + message);
            }
        }
    }

    [Conditional("ENABLE_LOGS")]
    public void Log(string message, bool showLogOnInGame = false, LogLayer mode = LogLayer.Developer)
    {
        UnityEngine.Debug.Log(message);
        if (showLogOnInGame && Application.isPlaying)
        {
            if (mode >= currentMode)
            {
                ShowLogOnUI(message);
            }
        }
    }

    [Conditional("ENABLE_LOGS")]
    public void LogWarning(string message, bool showLogOnInGame = false, LogLayer mode = LogLayer.Developer)
    {
        UnityEngine.Debug.LogWarning(message);
        if (showLogOnInGame && Application.isPlaying)
        {
            if (mode >= currentMode)
            {
                ShowLogOnUI("WARNING:".TextMod("yellow", true, true) + message);
            }
        }
    }
    private void ShowLogOnUI(string message)
    {
        var ingameUI = InGameLogUI.GetInstance();
        if (ingameUI)
        {
            InGameLogUI.GetInstance(forceCreate: false).ShowLog(message);
        }
        else
        {
            this.LogWarning("Trying to show log in the GameUI Console without having the InGameUI Prefab in the Scene");
        }
    }

}
