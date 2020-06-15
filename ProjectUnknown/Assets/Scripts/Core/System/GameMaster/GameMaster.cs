﻿using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class GameMaster : SingletonMonobehavior<GameMaster>
{
    private const string MASTER_SCENE = "MasterScene";
    private const string ENTITIES_SCENE = "EntitiesScene";
    private const string IN_GAME_MENU_SCENE = "InGameMenu";



    private const string MAIN_MENU_SCENE = "MainMenu";
    [SerializeField]
    [Required]
    StateManager gameStateManager = null;

    [SerializeField]
    [Required]
    GameMasterSettings masterSettings = null;
    [SerializeField]
    LevelSettings currentSettings = null;
    [SerializeField]
    LevelSettings startLevelSettings = null;

    [SerializeField]
    LevelSettings savedSettings = null;

    [SerializeField]
    [Required]
    LoadingControl loadingControl = null;

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    [SerializeField]
    UnityEvent levelLoadEvent = new UnityEvent();

    void Start()
    {
        UnloadAllScenesExcept(MASTER_SCENE);
        SaveLoadManager.LoadAllData();
        Screen.fullScreenMode = masterSettings.mode;
        if (masterSettings.skipMainMenu)
        {
            this.StartNewGame();
        }
        else
        {
            this.GoToMainMenu();
        }
    }

    public void StartNewGame()
    {
        RefreshSave();
        SaveLoadManager.SaveAllData();
        var startLevel = GetStartLevel();
        InitiateLoadLevelSequence(startLevel);
    }

    public void RefreshSave()
    {
        masterSettings.Reset();
        savedSettings.Reset();
    }

    public void LoadLevelAtSpawn(TransitionDoorProfile spawn)
    {
        this.currentSettings.startLevelDoor = spawn;
        this.InitiateLoadLevelSequence(currentSettings.startLevelDoor);
    }


    public Vector3 GetSpawnLocation()
    {
        return this.currentSettings.startLevelDoor.doorLocation;
    }

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
        if (gameStateManager.RequestState(GameState.GameStateEnum.Loading) == false) return;

        SaveLoadManager.SaveAllData();

        loadingControl.FadeIn(() =>
        {
            UnloadAllScenesExcept(MASTER_SCENE);
            LoadSceneAdditively(MAIN_MENU_SCENE);
            StartCoroutine(GetLevelLoadProcess(GameState.GameStateEnum.MainMenu));
        });
    }

    private void UnloadAllScenesExcept(string sceneNotToUnloadName)
    {
        int numOfScene = SceneManager.sceneCount;
        LogHelper.GetInstance().Log(MASTER_SCENE.Bolden().Colorize(Color.green) + " counts " + numOfScene + " at start", true);
        for (int i = 0; i < numOfScene; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != sceneNotToUnloadName)
            {
                LogHelper.GetInstance().Log(MASTER_SCENE.Bolden().Colorize(Color.green) + " unloading " + scene.name, true);
                UnloadScene(scene.name);
            }
        }
    }

    public void LoadSceneAdditively(string sceneName)
    {
        LogHelper.GetInstance().Log(" Loading Additively " + sceneName.Bolden() + "", true);
        this.scenesLoading.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
        if (SceneManager.GetActiveScene().name.Equals(MASTER_SCENE) == false)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(MASTER_SCENE));
        }

    }

    public void LoadLevelWithLandingDoor(TransitionDoorProfile profileToland)
    {
        InitiateLoadLevelSequence(profileToland, true);
    }

    public TransitionDoorProfile GetStartLevel()
    {
        return startLevelSettings.startLevelDoor;
    }

    public void InitiateLoadLevelSequence(TransitionDoorProfile profileToland, bool saveWhenEnter = true)
    {
        if (gameStateManager.RequestState(GameState.GameStateEnum.Loading) == false) return;

        this.currentSettings.startLevelDoor = profileToland;
        if (saveWhenEnter)
        {
            savedSettings.SaveDoorAsStartSpawn(profileToland);
            SaveLoadManager.SaveAllData();
        }

        this.levelLoadEvent.Invoke();
        loadingControl.FadeIn(() =>
        {
            StartCoroutine(SaveLoadCoroutine(() =>
            {
                LoadLevel(profileToland.doorHome);
                StartCoroutine(GetLevelLoadProcess(GameState.GameStateEnum.InGame));
            }));
        });
    }
    public IEnumerator SaveLoadCoroutine(Action callback)
    {
        SaveLoadManager.LoadAllData();
        yield return null;
        callback();
    }

    public void LoadLevel(string levelName)
    {
        UnloadAllScenesExcept(MASTER_SCENE);

        SFXSystem.GetInstance().StopAllSounds();
        LoadSceneAdditively(levelName);
        this.currentSettings.currentLevel = levelName;

        LoadSceneAdditively(ENTITIES_SCENE);
        LoadSceneAdditively(IN_GAME_MENU_SCENE);
    }

    public IEnumerator GetLevelLoadProcess(GameState.GameStateEnum gamestateAfterLoad)
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                yield return null;
            }
        }


        loadingControl.FadeOut(() =>
        {
            gameStateManager.RequestState(gamestateAfterLoad);
        });
        yield return null;
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
        SaveLoadManager.SaveAllData();
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
        this.InitiateLoadLevelSequence(this.currentSettings.startLevelDoor, false);
    }

    public void SetGameTimeScale(float newTimeScale)
    {
        Time.timeScale = newTimeScale;
    }
    public LevelSettings GetCurrentLevelSettings()
    {
        return this.currentSettings;
    }
    public void UpdateLevelSettingsBounds(Vector2 origin, Vector3 bounds)
    {
        this.currentSettings.levelCenter = origin;
        this.currentSettings.levelBounds = bounds;
    }
    public void UpdateCurrentLevelSettings(TransitionDoorProfile transitionDoorProfile)
    {
        this.currentSettings.startLevelDoor = transitionDoorProfile;
    }
}
