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
    ShakeData data = null;
    [BoxGroup("Requirements")]
    [SerializeField]
    bool shakeRotate = false;

    private Vector2 originPos;
    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    float currentShakeDuration = 0.0f;



    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    System.Action callback = null;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    bool isShaking = false;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    float currentTrauma = 0;
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
            if (currentTrauma >= 0)
            {
                data.shakeAmount = currentTrauma * currentTrauma;
                if (data.usePerlin)
                {
                    var offX = originPos.x * Mathf.PerlinNoise(0, Time.time) * data.shakeAmount;
                    var offy = originPos.y * Mathf.PerlinNoise(1, Time.time) * data.shakeAmount;
                    targetObject.transform.localPosition = new Vector2(offX, offy);
                }
                else
                {
                    targetObject.transform.localPosition = originPos + Random.insideUnitCircle * data.shakeAmount;
                }
                if (shakeRotate)
                {
                    targetObject.transform.localRotation = Quaternion.Euler(Random.insideUnitSphere * data.shakeAmount);
                }
                currentTrauma -= Time.deltaTime * data.decreaseFactor;
            }
            else
            {
                targetObject.transform.localPosition = originPos;
                if (shakeRotate)
                {
                    targetObject.transform.localRotation = Quaternion.identity;
                }
                isShaking = false;
                data.shakeAmount = 0;
                if (callback != null)
                {
                    callback();
                }
            }
        }

    }
    public void InduceTrauma(System.Action callback)
    {
        originPos = targetObject.transform.localPosition;
        this.callback = callback;
        isShaking = true;
        currentShakeDuration = data.shakeDuration;
        currentTrauma = data.trauma;
    }
    public void InduceTrauma()
    {
        InduceTrauma(null);
    }
}