using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class GameSystemMenu
{
    const string PLAYER_INTERACTABLE_PATH = "Prefabs/EnvirontmenInteractable/PlayerInteractables/";
    const string COLLECTABLES_PATH = "Prefabs/Collectables/";
    const string AXE_INTERACTABLE_PATH = "Prefabs/EnvirontmenInteractable/AxeInteractables/";
    const string ENVIRONMENT_PATH = "Prefabs/EnvironmentElements/Animated/";
    public static GameObject SpawnInstanceInScene(string path)
    {
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>(path));
        obj.transform.SetParent(Selection.activeGameObject.transform);
        obj.transform.position = GetScenePosition();
        Selection.activeGameObject = obj;
        return obj;

    }
    [MenuItem("GameObject/2D Object/Game System/Arrow Spawner")]
    public static void CreateArrowSpawner()
    {
        SpawnInstanceInScene(PLAYER_INTERACTABLE_PATH + "ArrowSpawner");
    }
    [MenuItem("GameObject/2D Object/Game System/Level Door")]
    public static void CreateLevelDoor()
    {
        SpawnInstanceInScene(PLAYER_INTERACTABLE_PATH + "Door");
    }

    [MenuItem("GameObject/2D Object/Game System/Timed Door")]
    public static void CreateDoor()
    {
        SpawnInstanceInScene(AXE_INTERACTABLE_PATH + "DoorEntity");
    }
    [MenuItem("GameObject/2D Object/Game System/DoorLevel")]
    public static void CreateDoorLever()
    {
        SpawnInstanceInScene(AXE_INTERACTABLE_PATH + "DoorLever");
    }
    [MenuItem("GameObject/2D Object/Game System/RailBlock-Horitzontal")]
    public static void CreateRailBlock()
    {
        SpawnInstanceInScene(AXE_INTERACTABLE_PATH + "RailBlockEntity-Horizontal");
    }
    [MenuItem("GameObject/2D Object/Game System/RailBlock-Vertical")]
    public static void CreateRailBlockVertical()
    {
        SpawnInstanceInScene(AXE_INTERACTABLE_PATH + "RailBlockEntity-Vertical");
    }
    [MenuItem("GameObject/2D Object/Game System/TeleGem")]
    public static void CreateTeleGem()
    {
        SpawnInstanceInScene(AXE_INTERACTABLE_PATH + "TeleGem_prfb");
    }
    [MenuItem("GameObject/2D Object/Game System/Pot/PotA")]
    public static void CreatePotA()
    {
        SpawnInstanceInScene(AXE_INTERACTABLE_PATH + "PotA_prfb");
    }
    [MenuItem("GameObject/2D Object/Game System/Pot/PotB")]
    public static void CreatePotB()
    {
        SpawnInstanceInScene(AXE_INTERACTABLE_PATH + "PotB_prfb");
    }
    [MenuItem("GameObject/2D Object/Game System/Pot/PotC")]
    public static void CreatePotC()
    {
        SpawnInstanceInScene(AXE_INTERACTABLE_PATH + "PotC_prfb");
    }
    [MenuItem("GameObject/2D Object/Game System/Pot/PotD")]
    public static void CreatePotD()
    {
        SpawnInstanceInScene(AXE_INTERACTABLE_PATH + "PotD_prfb");
    }
    [MenuItem("GameObject/2D Object/Game System/Pot/PotE")]
    public static void CreatePotE()
    {
        SpawnInstanceInScene(AXE_INTERACTABLE_PATH + "PotE_prfb");
    }
    [MenuItem("GameObject/2D Object/Game System/Decoration/Vine")]
    public static void CreateVine()
    {
        SpawnInstanceInScene(AXE_INTERACTABLE_PATH + "Vine");
    }
    [MenuItem("GameObject/2D Object/Game System/Decoration/Grass")]
    public static void Grass()
    {
        SpawnInstanceInScene(AXE_INTERACTABLE_PATH + "GrassClump");
    }
    [MenuItem("GameObject/2D Object/Game System/Decoration/GearRailA")]
    public static void RailGearA()
    {
        SpawnInstanceInScene(ENVIRONMENT_PATH + "RailGears_A_prfb");
    }
    [MenuItem("GameObject/2D Object/Game System/Decoration/GearRailB")]
    public static void RailGearB()
    {
        SpawnInstanceInScene(ENVIRONMENT_PATH + "RailGears_B_prfb");
    }
    [MenuItem("GameObject/2D Object/Game System/Decoration/FirePlace")]
    public static void FirePlace()
    {
        SpawnInstanceInScene(ENVIRONMENT_PATH + "LightFire_prfb");
    }
    [MenuItem("GameObject/2D Object/Game System/Decoration/Waterfall")]
    public static void WaterFall()
    {
        SpawnInstanceInScene(ENVIRONMENT_PATH + "WallWater_prfb");
    }
    [MenuItem("GameObject/2D Object/Game System/Collectables/Statuette")]
    public static void Statuette()
    {
        SpawnInstanceInScene(COLLECTABLES_PATH + "StatuetteCollectable_prfb");
    }
    [MenuItem("GameObject/2D Object/Game System/Invisible-CheckPoint")]
    public static void CheckPoint()
    {
        SpawnInstanceInScene(PLAYER_INTERACTABLE_PATH + "Door-Invisible-CheckPoint");
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

    [MenuItem("Game Master/Add Master Scene")]
    public static void AddMasterScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/SetupScene/MasterScene.unity", OpenSceneMode.Additive);
    }

    [MenuItem("Game Master/Add In Game Menu")]
    public static void AddInGameMenu()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/UIScenes/InGameMenu.unity", OpenSceneMode.Additive);
    }
    private static Vector3 GetScenePosition()
    {
        Camera sceneCam = SceneView.lastActiveSceneView.camera;
        Vector3 pos = sceneCam.transform.position;
        pos.z = 0;
        return pos;
    }
}
