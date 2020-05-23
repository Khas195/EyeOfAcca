using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : SingletonMonobehavior<GameMaster>
{
    [SerializeField]
    [Required]
    StateManager gameStateManager = null;

    [SerializeField]
    [Required]
    GameMasterSettings settings = null;



    [SerializeField]
    int doorIndex = 0;
    [SerializeField]
    [ReadOnly]
    string currentLevel = "";
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        UnloadAllScenesExcept("MasterScene");

        if (settings.skipMainMenu)
        {
            this.LoadLevel(settings.startLevel);
        }
        else
        {
            this.GoToMainMenu();
        }
    }

    public void ReloadCurrentLevel()
    {
        LoadLevel(currentLevel);
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
        this.scenesLoading.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
        if (SceneManager.GetActiveScene().name.Equals("MasterScene") == false)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("MasterScene"));
        }

    }

    public void LoadLevelWithLandingIndex(string targetScene, int doorIndex)
    {
        this.doorIndex = doorIndex;
        LoadLevel(targetScene);
    }

    public string GetStartLevel()
    {
        return settings.startLevel;
    }

    public void LoadLevel(string levelName)
    {
        UnloadAllScenesExcept("MasterScene");

        SFXSystem.GetInstance().StopAllSounds();
        SaveLoadManager.LoadAllData();
        LoadSceneAdditively(levelName);
        currentLevel = levelName;
        gameStateManager.RequestState(GameState.GameStateEnum.InGame);

        LoadSceneAdditively("EntitiesScene");
        LoadSceneAdditively("InGameMenu");
        StartCoroutine(GetLevelLoadProcess());
    }
    public IEnumerator GetLevelLoadProcess()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                yield return null;
            }
        }
        StageLevel();
        yield return null;
    }

    public void StageLevel()
    {
        var player = PlayerController2D.GetInstance(false);
        if (player == null)
        {
            LogHelper.GetInstance().LogError("Loading Level without Entities Scene");
            return;
        }
        var level = Level.GetInstance(false);
        if (level == null)
        {
            LogHelper.GetInstance().LogError("Loading Level without Level Script");
            return;

        }
        var camera = CameraFollow.GetInstance(false);
        if (camera == null)
        {
            LogHelper.GetInstance().LogError("Loading Level without Camera Follow");
            return;

        }

        var landingPosition = level.GetDoor(doorIndex).transform.position;
        player.SetLandingPosition(landingPosition);
        camera.SetPosition(landingPosition);
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


    public StateManager GetStateManager()
    {
        return gameStateManager;
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
        LogHelper.GetInstance().Log(("Restarting Level: " + SceneManager.GetActiveScene().name).Bolden(), true);
        this.LoadLevel(SceneManager.GetActiveScene().name);
    }

    public void SetGameTimeScale(float newTimeScale)
    {
        Time.timeScale = newTimeScale;
    }
}
