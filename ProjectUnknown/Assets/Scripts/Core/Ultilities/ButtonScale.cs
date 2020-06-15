using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class ButtonScale : MonoBehaviour
{
    [BoxGroup("Settings")]
    [SerializeField]
    float normalScale = 1.0f;
    [BoxGroup("Settings")]
    [SerializeField]
    float highlightScale = 1.3f;
    [BoxGroup("Settings")]
    [SerializeField]
    float pressScale = 1.2f;

    [BoxGroup("Settings")]
    [SerializeField]
    float highlightTime = 0.7f;
    [BoxGroup("Settings")]
    [SerializeField]
    float pressTime = 0.3f;

    [SerializeField]
    [BoxGroup("Status")]
    [ReadOnly]
    float step = 0;

    [SerializeField]
    [BoxGroup("Status")]
    [ReadOnly]
    float curTime = 0;

    [SerializeField]
    [BoxGroup("Status")]
    [ReadOnly]
    float targetTime = 0;
    [SerializeField]
    [BoxGroup("Status")]
    [ReadOnly]
    float currentScale = 0;
    [SerializeField]
    [BoxGroup("Status")]
    [ReadOnly]
    float targetScale = 0;

    void Start()
    {
        currentScale = normalScale;
        targetScale = normalScale;
    }

    void Update()
    {
        if (curTime < targetTime)
        {
            var scale = currentScale + step;
            SetScale(scale);
            curTime += Time.deltaTime;
        }
        else
        {
            SetScale(targetScale);
        }
    }
    public void SetScale(float scale)
    {
        var localScale = this.transform.localScale;
        localScale.x = localScale.y = localScale.z = scale;
        this.transform.localScale = localScale;
    }
    public void Hightlight()
    {
        ScaleTo(highlightScale, highlightTime);
    }

    public void StopHightLight()
    {
        ScaleTo(normalScale, highlightTime);
    }
    public void OnPressed()
    {

        ScaleTo(pressScale, pressTime);
    }
    public void OnRelease()
    {

        ScaleTo(normalScale, pressTime);
    }
    private void ScaleTo(float newScale, float time)
    {
        curTime = 0;
        targetScale = newScale;
        targetTime = time;
        step = (targetScale - currentScale) / targetTime;
    }
}
