using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class SceneChooser
{
    [Scene]
    public string sceneName;
}
public class GameMaster : SingletonMonobehavior<GameMaster>
{
    [SerializeField]
    bool skipMainMenu = false;
    [SerializeField]
    [Required]
    StateManager gameStateManager = null;

    [SerializeField]
    [Scene]
    string startLevel = "";

    [SerializeField]
    int currentInGameLevelIndex = 1;



    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        UnloadAllScenesExcept("MasterScene");

        if (skipMainMenu)
        {
            this.LoadLevel(startLevel);
        }
        else
        {
            this.GoToMainMenu();
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameStateManager.GetCurrentState().GetEnum().Equals(GameState.GameStateEnum.GamePaused))
            {
                UnPauseGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        this.gameStateManager.RequestState(GameState.GameStateEnum.GamePaused);
    }
    public void UnPauseGame()
    {
        this.gameStateManager.RequestState(GameState.GameStateEnum.InGame);
    }
    public void GoToMainMenu()
    {
        UnloadAllScenesExcept("MasterScene");
        LoadSceneAdditively("MainMenu");
        gameStateManager.RequestState(GameState.GameStateEnum.MainMenu);
    }

    private void UnloadAllScenesExcept(string sceneNotToUnloadName)
    {
        int numOfScene = SceneManager.sceneCount;
        LogHelper.GetInstance().Log("Game Master".Bolden().Colorize(Color.green) + " counts " + numOfScene + " at start", true);
        for (int i = 0; i < numOfScene; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != sceneNotToUnloadName)
            {
                LogHelper.GetInstance().Log("Game Master".Bolden().Colorize(Color.green) + " unloading " + scene.name, true);
                UnloadScene(scene.name);
            }
        }
    }

    public void LoadSceneAdditively(string sceneName)
    {
        LogHelper.GetInstance().Log(" Loading Additively " + sceneName.Bolden() + "", true);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        if (SceneManager.GetActiveScene().name.Equals("MasterScene") == false)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("MasterScene"));
        }

    }
    public void LoadLevel(int levelIndex)
    {
        LoadLevel("Level" + levelIndex);
    }
    public string GetStartLevel()
    {
        return startLevel;
    }

    private void LoadPrequisiteScenesForLevel()
    {
        UnloadAllScenesExcept("MasterScene");
        LoadSceneAdditively("EntitiesScene");
        LoadSceneAdditively("InGameMenu");
    }

    public void LoadLevel(string levelName)
    {
        SaveLoadManager.LoadAllData();
        LoadPrequisiteScenesForLevel();
        LoadSceneAdditively(levelName);
        currentInGameLevelIndex = (int)Char.GetNumericValue(levelName.ToCharArray()[levelName.Length - 1]);
        gameStateManager.RequestState(GameState.GameStateEnum.InGame);
    }

    private void UnloadCurrentLevel()
    {
        int numOfScene = SceneManager.sceneCount;
        for (int i = 0; i < numOfScene; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name.Contains("Level"))
            {
                UnloadScene(scene.name);
            }
        }
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void LoadNextLevel()
    {
        int nextLevelIndex = currentInGameLevelIndex + 1;
        LogHelper.GetInstance().Log("Checking if level " + nextLevelIndex + " can be loaded", true);
        if (Application.CanStreamedLevelBeLoaded("Level" + nextLevelIndex))
        {
            LoadLevel(nextLevelIndex);
            currentInGameLevelIndex = nextLevelIndex;
        }
        else
        {
            LogHelper.GetInstance().LogWarning("Level " + nextLevelIndex + " could not be loaded", true);
            LogHelper.GetInstance().Log("Restarting current level instead", true);
            RestartLevel();
        }
    }

    public void SetMouseVisibility(bool visibility)
    {
        Cursor.visible = visibility;
        Cursor.lockState = visibility ? CursorLockMode.None : CursorLockMode.Confined;
    }

    public void ResumeGame()
    {
        this.gameStateManager.RequestState(GameState.GameStateEnum.InGame);
    }


    public void ExitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void SetCurrentInGameLevel(int levelIndex)
    {
        currentInGameLevelIndex = levelIndex;
    }
    public StateManager GetStateManager()
    {
        return gameStateManager;
    }

    public int GetInGameLevelIndex()
    {
        return currentInGameLevelIndex;
    }
    public bool IsInState(GameState.GameStateEnum stateToCheck)
    {
        if (gameStateManager == null)
        {
            return false;
        }
        else
        {
            return gameStateManager.GetCurrentState().GetEnum().Equals(stateToCheck);
        }
    }
    public void RestartLevel()
    {
        LogHelper.GetInstance().Log(("Restarting Level: " + currentInGameLevelIndex).Bolden(), true);
        this.LoadLevel(currentInGameLevelIndex);
    }

    public void SetGameTimeScale(float newTimeScale)
    {
        Time.timeScale = newTimeScale;
    }
}
