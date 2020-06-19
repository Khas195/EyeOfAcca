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
    [Required]
    GameObject collectableRoot = null;
    [SerializeField]
    [Required]
    LevelCollectablesData collectableData = null;
    [SerializeField]
    [ReorderableList]
    [ReadOnly]
    [OnValueChanged("OnEdit")]
    List<GameObject> collectables = new List<GameObject>();

    void Start()
    {
        var gameMaster = GameMaster.GetInstance();
        if (gameMaster)
        {
            gameMaster.UpdateLevelSettingsBounds(this.GetGroundMapPosition(), this.GetGroundMapBounds());
            gameMaster.UpdateLevelCurrentCollectables(this.collectableData);
        }
        for (int i = 0; i < collectableData.datas.Count; i++)
        {
            if (collectableData.datas[i].IsCollected)
            {
                collectables[i].SetActive(false);
            }
            else
            {
                collectables[i].GetComponent<Collectable>().OnCollect.AddListener(this.OnCollectCollectable);
            }
        }

    }
    public void OnCollectCollectable(Collectable collect)
    {
        for (int i = 0; i < collectables.Count; i++)
        {
            if (collectables[i].Equals(collect.gameObject))
            {
                collectables[i].SetActive(false);
                collectableData.datas[i].Collect();
                break;
            }
        }
    }
#if UNITY_EDITOR
    [Button("Create Data")]
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
        string path = "Prefabs/Collectables/StatuetteCollectable_prfb";
        GameObject obj = (GameObject)UnityEditor.PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>(path));
        obj.transform.SetParent(collectableRoot.transform);
        obj.transform.position = GetScenePosition();
        UnityEditor.Selection.activeGameObject = obj;

        collectables.Add(obj);
        collectableData.datas.Add(new LevelCollectablesData.CollectableData(false));

        UnityEditor.EditorUtility.SetDirty(this.collectableData);
    }

    [Button("SAVE CHANGES")]
    public void SaveChanges()
    {
        collectableData.datas.Clear();
        for (int i = 0; i < collectables.Count; i++)
        {
            collectableData.datas.Add(new LevelCollectablesData.CollectableData(false));
        }
        this.collectableData.SaveData();
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
