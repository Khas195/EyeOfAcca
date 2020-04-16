using System;
using System.Collections.Generic;
using System.Diagnostics;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class InGameLogUI : SingletonMonobehavior<InGameLogUI>
{
    [SerializeField]
    [Required]
    GridLayoutGroup layout = null;
    [SerializeField]
    [Required]
    LogItemPool logItemPool = null;

    [SerializeField]
    float existTime = 1f;
    [SerializeField]
    float fadeTime = 1f;
    List<LogTextItem> logList = new List<LogTextItem>();


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Start()
    {
        var rect = this.layout.GetComponent<RectTransform>();
        this.layout.cellSize = new Vector2(rect.rect.width, rect.rect.height / 30f);
    }

    public void ShowLog(string log)
    {
        var newLog = logItemPool.RequestInstance();
        var textComponent = newLog.item;
        textComponent.text = log;
        this.logList.Add(newLog);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLogs();
    }

    private void UpdateLogs()
    {
        for (int i = 0; i < logList.Count; i++)
        {
            var log = logList[i];
            log.item.transform.localScale = Vector3.one;
            if (log.currentExistTime < existTime)
            {
                log.currentExistTime += Time.deltaTime;
                logList[i] = log;
            }
            else
            {
                if (log.currentFadeTime <= fadeTime)
                {
                    log.currentFadeTime += Time.deltaTime;
                    FadeLogItem(log);
                    logList[i] = log;
                }
                if (log.item.color.a <= 0.2f)
                {
                    RemoveLog(log);
                }
            }
        }
    }

    private void FadeLogItem(LogTextItem log)
    {
        var newFadeColor = log.item.color;
        var blend = Mathf.Clamp01(log.currentFadeTime / fadeTime);
        newFadeColor.a = Mathf.Lerp(newFadeColor.a, 0, blend);
        log.item.color = newFadeColor;
    }

    private void RemoveLog(LogTextItem log)
    {
        logList.Remove(log);
        logItemPool.ReturnInstance(log);
    }
}
