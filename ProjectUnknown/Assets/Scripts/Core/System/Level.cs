using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Level : MonoBehaviour
{
    [SerializeField]
    [Required]
    Tilemap groundTileMap = null;
    [SerializeField]
    [ReadOnly]
    GameObject collectableRoot = null;
    [SerializeField]
    [ReadOnly]
    GameObject indicatorManagerRoot = null;
    [SerializeField]
    [ReadOnly]
    GameObject skeletonRoot = null;
    [SerializeField]
    [ReadOnly]
    LevelCollectablesData collectableData = null;

    [SerializeField]
    [ReadOnly]
    LevelDeadData deadPlaceDatas = null;


    [SerializeField]
    [ReorderableList]
    [ReadOnly]
    List<GameObject> collectables = new List<GameObject>();
    [SerializeField]
    [ReorderableList]
    [ReadOnly]
    List<CollectablesIndicatorManager> indicatorManagers = new List<CollectablesIndicatorManager>();

    [SerializeField]
    [ShowAssetPreview(64, 64)]
    GameObject skeletonPrefabs;
    void Start()
    {
        CheckInWithGameMaster();

        Collectable.OnCollect.AddListener(this.OnCollectCollectable);

        CleanList();
#if UNITY_EDITOR
        if (collectableData == null)
        {
            this.CreateData();
        }
        if (deadPlaceDatas == null)
        {
            this.CreateDeadData();
        }
#endif
        Setup();
        SaveChanges();
    }
    [Button("SAVE CHANGES")]
    public void SaveChanges()
    {
        this.collectableData.SaveData();
        this.deadPlaceDatas.SaveData();
    }
    private void Setup()
    {
        TurnOffCollectedStatue();
        SynchronizeCollectbles();
        CreateIndicatorManagerRoot();
        UpdateIndicatorManagers();
        Chip.OnChracterDeadGlobal.AddListener(OnCharacterDead);
        if (skeletonPrefabs == null)
        {
            FindDeadBodyPrefab();
        }
        CreateSkeletonRoot();
        SpawnSkeletonsAtDeadPlace();
    }

    private void SynchronizeCollectbles()
    {
        if (collectables.Count > collectableData.datas.Count)
        {
            var neededAmount = collectables.Count - collectableData.datas.Count;
            for (int i = 0; i < neededAmount; i++)
            {
                collectableData.datas.Add(new LevelCollectablesData.CollectableData(false));
            }
        }
        else if (collectables.Count < collectableData.datas.Count)
        {
            var extraAmount = collectableData.datas.Count - collectables.Count;
            collectableData.datas.RemoveRange(collectableData.datas.Count - extraAmount, extraAmount);
        }
    }

    public void OnCharacterDead(Vector2 place)
    {
        if (deadPlaceDatas == null) return;
        LogHelper.GetInstance().Log("Dead Place added");
        deadPlaceDatas.deadPlaces.Add(place);
        deadPlaceDatas.SaveData();
    }

    private void SpawnSkeletonsAtDeadPlace()
    {
        if (deadPlaceDatas == null) return;
        for (int i = 0; i < deadPlaceDatas.deadPlaces.Count; i++)
        {
            var newSkeleton = GameObject.Instantiate(skeletonPrefabs, deadPlaceDatas.deadPlaces[i], Quaternion.identity, this.skeletonRoot.transform);
        }
    }

    private void UpdateIndicatorManagers()
    {
        // setup indicator managers
        for (int i = 0; i < indicatorManagers.Count; i++)
        {
            indicatorManagers[i].UpdateActivatedIndicators(this.collectableData);
        }
    }

    private void TurnOffCollectedStatue()
    {
        for (int i = 0; i < collectableData.datas.Count; i++)
        {
            if (collectableData.datas[i].IsCollected)
            {
                collectables[i].SetActive(false);
            }
        }
    }

    private void CleanList()
    {
        CleanCollectableIndicatorList();
        CleanCollectableList();
    }

    private void CheckInWithGameMaster()
    {
        var gameMaster = GameMaster.GetInstance();
        if (gameMaster)
        {
            gameMaster.UpdateLevelSettingsBounds(this.GetGroundMapPosition(), this.GetGroundMapBounds());
            gameMaster.UpdateLevelCurrentCollectables(this.collectableData);
        }
    }

    public void OnCollectCollectable(Collectable collect)
    {
        for (int i = 0; i < collectables.Count; i++)
        {
            if (collectables[i].Equals(collect.gameObject))
            {
                collectableData.datas[i].Collect();
                break;
            }
        }
        for (int i = 0; i < indicatorManagers.Count; i++)
        {
            indicatorManagers[i].IncreaseActivatedIndicator();
        }
    }
    [Button("Clean Collectables List")]
    private void CleanCollectableList()
    {
        var collectableList = collectables.ToArray();
        for (int i = 0; i < collectableList.Length; i++)
        {
            if (collectableList[i] == null)
            {
                collectables.Remove(collectableList[i]);
            }
        }
    }
    [Button("Clean Collectables Indicator List")]
    private void CleanCollectableIndicatorList()
    {
        var managerList = indicatorManagers.ToArray();
        for (int i = 0; i < managerList.Length; i++)
        {
            if (managerList[i] == null)
            {
                indicatorManagers.Remove(managerList[i]);
            }
        }
    }
    [Button("Find skeleton Prefab")]
    public void FindDeadBodyPrefab()
    {
        this.skeletonPrefabs = Resources.Load("Prefabs/EnvironmentElements/Static/Skeleton_Down_Physics_prfb") as GameObject;
    }
    [Button("Create Dead Place Root")]
    public void CreateSkeletonRoot()
    {
        if (this.skeletonRoot != null) return;
        var root = new GameObject("SkeletonRoot");
        root.transform.SetParent(this.transform);
        this.skeletonRoot = root;
    }
    [Button("Create Indicator Manager Root")]
    public void CreateIndicatorManagerRoot()
    {
        if (this.indicatorManagerRoot != null) return;
        var root = new GameObject("IndicatorManagerRoot");
        root.transform.SetParent(this.transform);
        this.indicatorManagerRoot = root;
    }
#if UNITY_EDITOR

    [Button("Create Collectables Data")]

    public void CreateData()
    {
        var newCollectableData = ScriptableObject.CreateInstance<LevelCollectablesData>();
        var sceneName = this.gameObject.scene.name;
        UnityEditor.AssetDatabase.CreateAsset(newCollectableData, "Assets/Resources/Data/LevelColectablesData/" + sceneName + "-" + this.gameObject.name + ".asset");
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.EditorUtility.FocusProjectWindow();
        this.collectableData = newCollectableData;
        UnityEditor.EditorUtility.SetDirty(this.collectableData);
    }
    [Button("Create Dead Data")]
    public void CreateDeadData()
    {
        var newDeadData = ScriptableObject.CreateInstance<LevelDeadData>();
        var sceneName = this.gameObject.scene.name;
        UnityEditor.AssetDatabase.CreateAsset(newDeadData, "Assets/Resources/Data/LevelDeadData/" + sceneName + "-" + this.gameObject.name + "-DeadPlaces" + ".asset");
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.EditorUtility.FocusProjectWindow();
        this.deadPlaceDatas = newDeadData;
        UnityEditor.EditorUtility.SetDirty(newDeadData);

    }

    public void CreateCollectableRoot()
    {
        if (this.collectableRoot != null) return;

        var newRoot = new GameObject("CollectableRoot");
        newRoot.transform.SetParent(this.transform);
        this.collectableRoot = newRoot;
    }
    private Vector3 GetScenePosition()
    {
        Camera sceneCam = UnityEditor.SceneView.lastActiveSceneView.camera;
        Vector3 pos = sceneCam.transform.position;
        pos.z = 0;
        return pos;
    }
    [Button("Add Collectable")]
    public void AddCollectable()
    {
        if (this.collectableRoot == null)
        {
            CreateCollectableRoot();
        }
        if (this.collectableData == null)
        {
            CreateData();
        }
        string path = "Prefabs/Collectables/StatuetteCollectable_prfb";
        GameObject obj = (GameObject)UnityEditor.PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>(path));
        obj.transform.SetParent(collectableRoot.transform);
        obj.transform.position = GetScenePosition();

        CleanCollectableList();

        collectables.Add(obj);
        collectableData.datas.Add(new LevelCollectablesData.CollectableData(false));


        UnityEditor.EditorUtility.SetDirty(this.collectableData);
    }



    [Button("Add Collectable Indicator")]
    public void AddCollectableIndicator()
    {
        if (this.indicatorManagerRoot == null)
        {
            GameObject obj = new GameObject("IndicatorManagerRoot");
            obj.transform.SetParent(this.transform);
            this.indicatorManagerRoot = obj;
        }
        string path = "Prefabs/EnvironmentElements/Static/IndicatorManager_prfb";
        GameObject indicateManager = (GameObject)UnityEditor.PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>(path));
        indicateManager.transform.SetParent(this.indicatorManagerRoot.transform);
        CleanCollectableIndicatorList();
        indicatorManagers.Add(indicateManager.GetComponent<CollectablesIndicatorManager>());
    }



#endif
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundTileMap.transform.position + groundTileMap.cellBounds.center, groundTileMap.cellBounds.size);
    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>

    public Vector2 GetGroundMapPosition()
    {
        return groundTileMap.transform.position + groundTileMap.cellBounds.center;
    }

    public Vector3 GetGroundMapBounds()
    {
        return groundTileMap.cellBounds.size;
    }
}
