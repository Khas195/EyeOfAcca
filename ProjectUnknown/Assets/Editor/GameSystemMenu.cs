﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class GameSystemMenu
{
    [MenuItem("GameObject/3D Object/Game System/Arrow Spawner")]
    public static void CreateArrowSpawner()
    {
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Prefabs/EnvirontmenInteractable/PlayerInteractables/ArrowSpawner"));
        obj.transform.SetParent(Selection.activeGameObject.transform);
        obj.transform.position = GetScenePosition();
    }
    [MenuItem("GameObject/3D Object/Game System/Level Door")]
    public static void CreateLevelDoor()
    {
        var level = Level.GetInstance(false);
        if (level == null)
        {
            LogHelper.GetInstance().LogError("Cannot create a level door without the level.");
            return;
        }
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Prefabs/EnvirontmenInteractable/PlayerInteractables/Door"));
        obj.transform.SetParent(Selection.activeGameObject.transform);
        obj.transform.position = GetScenePosition();
        level.AddDoor(obj.GetComponent<LevelTransitionDoor>());
    }

    [MenuItem("GameObject/3D Object/Game System/Timed Door")]
    public static void CreateDoor()
    {
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Prefabs/EnvirontmenInteractable/AxeInteractables/DoorEntity"));
        obj.transform.SetParent(Selection.activeGameObject.transform);
        obj.transform.position = GetScenePosition();
    }
    [MenuItem("GameObject/3D Object/Game System/DoorLevel")]
    public static void CreateDoorLever()
    {
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Prefabs/EnvirontmenInteractable/AxeInteractables/DoorLever"));
        obj.transform.SetParent(Selection.activeGameObject.transform);
        obj.transform.position = GetScenePosition();
    }
    [MenuItem("GameObject/3D Object/Game System/RailBlock")]
    public static void CreateRailBlock()
    {
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Prefabs/EnvirontmenInteractable/AxeInteractables/RailBlockEntity"));
        obj.transform.SetParent(Selection.activeGameObject.transform);
        obj.transform.position = GetScenePosition();
    }
    [MenuItem("GameObject/3D Object/Game System/TeleGem")]
    public static void CreateTeleGem()
    {
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Prefabs/EnvirontmenInteractable/AxeInteractables/TeleGem_prfb"));
        obj.transform.SetParent(Selection.activeGameObject.transform);
        obj.transform.position = GetScenePosition();
    }
    [MenuItem("GameObject/3D Object/Game System/Pot/PotA")]
    public static void CreatePotA()
    {
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Prefabs/EnvirontmenInteractable/AxeInteractables/PotA_prfb"));
        obj.transform.SetParent(Selection.activeGameObject.transform);
        obj.transform.position = GetScenePosition();
    }
    [MenuItem("GameObject/3D Object/Game System/Pot/PotB")]
    public static void CreatePotB()
    {
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Prefabs/EnvirontmenInteractable/AxeInteractables/PotB_prfb"));
        obj.transform.SetParent(Selection.activeGameObject.transform);
        obj.transform.position = GetScenePosition();
    }
    [MenuItem("GameObject/3D Object/Game System/Pot/PotC")]
    public static void CreatePotC()
    {
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Prefabs/EnvirontmenInteractable/AxeInteractables/PotC_prfb"));
        obj.transform.SetParent(Selection.activeGameObject.transform);
        obj.transform.position = GetScenePosition();
    }
    [MenuItem("GameObject/3D Object/Game System/Pot/PotD")]
    public static void CreatePotD()
    {
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Prefabs/EnvirontmenInteractable/AxeInteractables/PotD_prfb"));
        obj.transform.SetParent(Selection.activeGameObject.transform);
        obj.transform.position = GetScenePosition();
    }
    [MenuItem("GameObject/3D Object/Game System/Pot/PotE")]
    public static void CreatePotE()
    {
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Prefabs/EnvirontmenInteractable/AxeInteractables/PotE_prfb"));
        obj.transform.SetParent(Selection.activeGameObject.transform);
        obj.transform.position = GetScenePosition();
    }
    private static Vector3 GetScenePosition()
    {
        Camera sceneCam = SceneView.lastActiveSceneView.camera;
        Vector3 pos = sceneCam.transform.position;
        pos.z = 0;
        return pos;
    }
}