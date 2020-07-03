using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class CollectablesIndicatorManager : MonoBehaviour
{
    [SerializeField]
    [ReadOnly]
    List<CollectableIndicatorSwitch> indicators = new List<CollectableIndicatorSwitch>();
    int curCount = 0;

    public void IncreaseActivatedIndicator()
    {
        if (curCount >= indicators.Count) return;
        indicators[curCount].IndicatorActivate();
        curCount += 1;
    }
    public void UpdateActivatedIndicators(LevelCollectablesData levelCollectData)
    {
        int indicatorsNeedActivate = levelCollectData.GetTotalCollected();
        if (indicatorsNeedActivate > indicators.Count)
        {
            indicatorsNeedActivate = indicators.Count;
        }
        for (int i = 0; i < indicatorsNeedActivate; i++)
        {
            indicators[i].IndicatorActivate();
        }
    }
    [Button("Clean Indicators List")]
    private void CleanCollectableIndicatorList()
    {
        var managerList = indicators.ToArray();
        for (int i = 0; i < managerList.Length; i++)
        {
            if (managerList[i] == null)
            {
                indicators.Remove(managerList[i]);
            }
        }
    }
#if UNITY_EDITOR

    [Button("Add Indicators")]
    public void AddIndicators()
    {
        string path = "Prefabs/EnvironmentElements/Static/CollectableIndicator_prfb";
        GameObject indicateManager = (GameObject)UnityEditor.PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>(path));
        indicateManager.transform.SetParent(this.transform);
        indicateManager.transform.localPosition = Vector3.zero;
        CleanCollectableIndicatorList();
    }
#endif
}
