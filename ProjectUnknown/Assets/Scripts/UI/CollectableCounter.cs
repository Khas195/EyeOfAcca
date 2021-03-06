using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CollectableCounter : MonoBehaviour
{
    [SerializeField]
    [ReadOnly]
    List<LevelCollectablesData> collectablesList = new List<LevelCollectablesData>();
    [SerializeField]
    Text counterText = null;
    int totalCollectables = 0;
    [SerializeField]
    UnityEvent OnCollectableUpdated = new UnityEvent();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var obj in Resources.LoadAll("Data", typeof(LevelCollectablesData)))
        {
            var collectable = (LevelCollectablesData)obj;
            collectablesList.Add(collectable);
            totalCollectables += collectable.datas.Count;
        }
        Collectable.OnCollect.AddListener(this.UpdateCurrentCollectableCount);
        UpdateUI();
    }


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void UpdateCurrentCollectableCount(Collectable collectedItem)
    {
        UpdateUI();
        OnCollectableUpdated.Invoke();
    }

    private void UpdateUI()
    {
        var count = 0;
        for (int i = 0; i < collectablesList.Count; i++)
        {
            for (int j = 0; j < collectablesList[i].datas.Count; j++)
            {
                if (collectablesList[i].datas[j].IsCollected)
                {
                    count += 1;
                }
            }
        }
        counterText.text = (count + "/" + totalCollectables).Bolden();
    }
}
