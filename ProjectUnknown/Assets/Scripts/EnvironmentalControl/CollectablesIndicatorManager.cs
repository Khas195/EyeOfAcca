using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesIndicatorManager : MonoBehaviour
{
    [SerializeField]
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
        if (indicatorsNeedActivate >= indicators.Count)
        {
            indicatorsNeedActivate = indicators.Count;
        }
        for (int i = 0; i < indicatorsNeedActivate; i++)
        {
            indicators[i].IndicatorActivate();
        }
    }
}
