using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class GameSystemMenu
{
    [MenuItem("GameObject/2D Object/Game System/Arrow Spawner")]
    public static void CreateArrowSpawner()
    {
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Prefabs/EnvirontmenInteractable/PlayerInteractables/ArrowSpawner"));
        obj.transform.SetParent(Selection.activeGameObject.transform);
        obj.transform.position = GetScenePosition();
    }
    [MenuItem("GameObject/2D Object/Game System/Level Door")]
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

    [MenuItem("GameObject/2D Object/Game System/Timed Door")]
    public static void CreateDoor()
    {
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Prefabs/EnvirontmenInteractable/AxeInteractables/DoorEntity"));
        obj.transform.SetParent(Selection.activeGameObject.transform);
        obj.transform.position = GetScenePosition();
    }
    [MenuItem("GameObject/2D Object/Game System/DoorLevel")]
    public static void CreateDoorLever()
    {
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Prefabs/EnvirontmenInteractable/AxeInteractables/DoorLever"));
        obj.transform.SetParent(Selection.activeGameObject.transform);
        obj.transform.position = GetScenePosition();
    }
    [MenuItem("GameObject/2D Object/Game System/RailBlock")]
    public static void CreateRailBlock()
    {
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Prefabs/EnvirontmenInteractable/AxeInteractables/RailBlockEntity"));
        obj.transform.SetParent(Selection.activeGameObject.transform);
        obj.transform.position = GetScenePosition();
    }
    [MenuItem("GameObject/2D Object/Game System/TeleGem")]
    public static void CreateTeleGem()
    {
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Prefabs/EnvirontmenInteractable/AxeInteractables/TeleGem_prfb"));
        obj.transform.SetParent(Selection.activeGameObject.transform);
        obj.transform.position = GetScenePosition();
    }
    [MenuItem("GameObject/2D Object/Game System/Pot/PotA")]
    public static void CreatePotA()
    {
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Prefabs/EnvirontmenInteractable/AxeInteractables/PotA_prfb"));
        obj.transform.SetParent(Selection.activeGameObject.transform);
        obj.transform.position = GetScenePosition();
    }
    [MenuItem("GameObject/2D Object/Game System/Pot/PotB")]
    public static void CreatePotB()
    {
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Prefabs/EnvirontmenInteractable/AxeInteractables/PotB_prfb"));
        obj.transform.SetParent(Selection.activeGameObject.transform);
        obj.transform.position = GetScenePosition();
    }
    [MenuItem("GameObject/2D Object/Game System/Pot/PotC")]
    public static void CreatePotC()
    {
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Prefabs/EnvirontmenInteractable/AxeInteractables/PotC_prfb"));
        obj.transform.SetParent(Selection.activeGameObject.transform);
        obj.transform.position = GetScenePosition();
    }
    [MenuItem("GameObject/2D Object/Game System/Pot/PotD")]
    public static void CreatePotD()
    {
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Prefabs/EnvirontmenInteractable/AxeInteractables/PotD_prfb"));
        obj.transform.SetParent(Selection.activeGameObject.transform);
        obj.transform.position = GetScenePosition();
    }
    [MenuItem("GameObject/2D Object/Game System/Pot/PotE")]
    public static void CreatePotE()
    {
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Prefabs/EnvirontmenInteractable/AxeInteractables/PotE_prfb"));
        obj.transform.SetParent(Selection.activeGameObject.transform);
        obj.transform.position = GetScenePosition();
    }
    [MenuItem("GameObject/2D Object/Game System/Decoration/Vine")]
    public static void CreateVine()
    {
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Prefabs/EnvirontmenInteractable/AxeInteractables/Vine"));
        obj.transform.SetParent(Selection.activeGameObject.transform);
        obj.transform.position = GetScenePosition();
    }
    [MenuItem("GameObject/2D Object/Game System/Decoration/Grass")]
    public static void Grass()
    {
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Prefabs/EnvirontmenInteractable/AxeInteractables/GrassClump"));
        obj.transform.SetParent(Selection.activeGameObject.transform);
        obj.transform.position = GetScenePosition();
    }
    [MenuItem("Game Master/Play-Stop, But From Master Scene %`")]
    public static void PlayFromPrelaunchScene()
    {
        if (EditorApplication.isPlaying == true)
        {
            EditorApplication.isPlaying = false;
            return;
        }

        EditorSceneManager.OpenScene("Assets/Scenes/SetupScene/MasterScene.unity", OpenSceneMode.Additive);
        EditorApplication.isPlaying = true;
    }

    [MenuItem("Game Master/Add Entities Scene")]
    public static void AddEntitiesScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/SetupScene/EntitiesScene.unity", OpenSceneMode.Additive);
    }
    private static Vector3 GetScenePosition()
    {
        Camera sceneCam = SceneView.lastActiveSceneView.camera;
        Vector3 pos = sceneCam.transform.position;
        pos.z = 0;
        return pos;
    }
}
