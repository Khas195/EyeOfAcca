using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
public class Shake : MonoBehaviour
{
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    GameObject targetObject = null;
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    ShakeData data;
    private Vector2 originPos;
    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    float currentShakeDuration = 0.0f;


    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    bool hasCallBack = false;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    System.Action callback = null;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    bool isShaking = false;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        currentShakeDuration = -1;
    }

    void Update()
    {
        if (isShaking)
        {
            if (currentShakeDuration >= 0)
            {
                targetObject.transform.localPosition = originPos + Random.insideUnitCircle * data.shakeAmount;
                currentShakeDuration -= Time.deltaTime * data.decreaseFactor;
            }
            else
            {
                targetObject.transform.localPosition = originPos;
                isShaking = false;
                if (callback != null)
                {
                    callback();
                }
            }
        }

    }
    public void InduceTrauma(System.Action callback)
    {
        originPos = targetObject.transform.position;
        hasCallBack = true;
        this.callback = callback;
        isShaking = true;
        currentShakeDuration = data.shakeDuration;
    }
}