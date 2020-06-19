using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelCollectableData", menuName = "Data/LevelCollectableData", order = 1)]
public class LevelCollectablesData : ScriptableObject, ISaveRestable
{
    [Serializable]
    public class CollectableData
    {

        [SerializeField]
        bool isCollected;

        public CollectableData(bool isCollected)
        {
            this.isCollected = isCollected;
        }

        public bool IsCollected { get => isCollected; }

        public void Collect()
        {
            this.isCollected = true;
        }
        public void ResetCollect()
        {
            this.isCollected = false;
        }
    }
    public List<CollectableData> datas = new List<CollectableData>();

    public void ResetSave()
    {
        LogHelper.GetInstance().Log(("Reset Collectable Data").Bolden(), true);
        for (int i = 0; i < datas.Count; i++)
        {
            datas[i].ResetCollect();
        }
    }
}
