using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class CollectableCounter : MonoBehaviour
{
    [SerializeField]
    [ReadOnly]
    LevelCollectablesData currentData = null;
    [SerializeField]
    Text counterText = null;
    // Start is called before the first frame update
    void Start()
    {
        TryGetData();
    }

    private bool TryGetData()
    {
        var gameMaster = GameMaster.GetInstance();
        if (gameMaster != null)
        {
            currentData = gameMaster.GetCurrentLevelSettings().currentCollectableData;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (currentData == null)
        {
            if (TryGetData() == false)
            {
                return;
            }

        }

        var count = 0;
        for (int i = 0; i < currentData.datas.Count; i++)
        {
            if (currentData.datas[i].IsCollected)
            {
                count++;
            }
        }
        counterText.text = (count + "/" + currentData.datas.Count).Bolden();
    }
}
