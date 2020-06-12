﻿using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class GameMaster : SingletonMonobehavior<GameMaster>
{
    [SerializeField]
    [Required]
    StateManager gameStateManager = null;

    [SerializeField]
    [Required]
    GameMasterSettings masterSettings = null;
    [SerializeField]
    LevelSettings levelSettings = null;
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
        UnloadAllScenesExcept("MasterScene");
        SaveLoadManager.LoadAllData();
        if (masterSettings.skipMainMenu)
        {
            LoadLevelAtSpawn(levelSettings.startSpawn);
        }
        else
        {
            this.GoToMainMenu();
        }
    }

    public void StartNewGame()
    {

        GameMaster.GetInstance().InitiateLoadLevelSequence(GameMaster.GetInstance().GetStartLevel(), newSave: true);
    }

    public void RefreshSave()
    {
        masterSettings.Reset();
        savedSettings.Reset();
    }

    public void LoadLevelAtSpawn(TransitionDoorProfile spawn)
    {
        this.levelSettings.currentSpawn = spawn;
        this.InitiateLoadLevelSequence(levelSettings.currentSpawn);
    }

    public void ReloadCurrentLevel()
    {
        InitiateLoadLevelSequence(this.levelSettings.currentSpawn);
    }

    public Vector3 GetSpawnLocation()
    {
        return this.levelSettings.currentSpawn.doorLocation;
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

        SaveLoadManager.LoadAllData();

        loadingControl.FadeIn(() =>
        {
            UnloadAllScenesExcept("MasterScene");
            LoadSceneAdditively("MainMenu");
            StartCoroutine(GetLevelLoadProcess(GameState.GameStateEnum.MainMenu));
        });
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

    public void LoadLevelWithLandingDoor(TransitionDoorProfile profileToland)
    {
        InitiateLoadLevelSequence(profileToland);
    }

    public TransitionDoorProfile GetStartLevel()
    {
        return levelSettings.startSpawn;
    }

    public void InitiateLoadLevelSequence(TransitionDoorProfile profileToland, bool newSave = false)
    {
        if (gameStateManager.RequestState(GameState.GameStateEnum.Loading) == false) return;

        this.levelLoadEvent.Invoke();
        loadingControl.FadeIn(() =>
        {
            this.levelSettings.currentSpawn = profileToland;
            StartCoroutine(SaveLoadCoroutine(newSave, () =>
            {
                LoadLevel(profileToland.doorHome);
                StartCoroutine(GetLevelLoadProcess(GameState.GameStateEnum.InGame));
            }));
        });
    }
    public IEnumerator SaveLoadCoroutine(bool newSave, Action callback)
    {
        if (newSave)
        {
            RefreshSave();
            SaveLoadManager.SaveAllData();
        }

        SaveLoadManager.LoadAllData();
        yield return null;
        callback();
    }

    public void LoadLevel(string levelName)
    {
        UnloadAllScenesExcept("MasterScene");

        SFXSystem.GetInstance().StopAllSounds();
        LoadSceneAdditively(levelName);
        this.levelSettings.currentLevel = levelName;

        LoadSceneAdditively("EntitiesScene");
        LoadSceneAdditively("InGameMenu");
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
        this.InitiateLoadLevelSequence(this.levelSettings.currentSpawn);
    }

    public void SetGameTimeScale(float newTimeScale)
    {
        Time.timeScale = newTimeScale;
    }
    public LevelSettings GetCurrentLevelSettings()
    {
        return this.levelSettings;
    }
    public void UpdateLevelSettingsBounds(Vector2 origin, Vector3 bounds)
    {
        this.levelSettings.levelCenter = origin;
        this.levelSettings.levelBounds = bounds;
    }
}
