using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [SerializeField]
    GameObject targetObject = null;
    [SerializeField]
    [InfoBox("The speed of the shake.", EInfoBoxType.Normal)]
    float frequency = 25;
    [SerializeField]
    Vector3 maximumTranslateShake = Vector3.one * 0.5f;

    [SerializeField]
    Vector3 maximumAngularShake = Vector3.one * 2;

    [SerializeField]
    float recoverySpeed = 1.5f;
    [SerializeField]
    float traumaExponent = 2;
    float trauma = 0;
    float seed = 0;

    Vector2 posOfTrauma = Vector2.one;
    bool hasCallBack = false;
    System.Action callback = null;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        seed = Random.value;
    }

    // Update is called once per frame
    void Update()
    {

        if (trauma <= 0.5f)
        {
            if (hasCallBack && callback != null)
            {
                this.callback();
                callback = null;
                hasCallBack = false;
            }
            return;
        }

        var shake = Mathf.Pow(trauma, traumaExponent);
        // return [0, 1]
        // Then translate it to [-1, 1]
        var randomPerlinX = maximumTranslateShake.x * ((Mathf.PerlinNoise(seed, Time.time * frequency) * 2) - 1);
        var randomPerlinY = maximumTranslateShake.y * ((Mathf.PerlinNoise(seed + 1, Time.time * frequency) * 2) - 1);

        targetObject.transform.position = posOfTrauma + new Vector2(randomPerlinX, randomPerlinY) * shake;

        var randomPerlinRotX = maximumAngularShake.x * ((Mathf.PerlinNoise(seed + 3, Time.time * frequency) * 2) - 1);
        var randomPerlinRotY = maximumAngularShake.y * ((Mathf.PerlinNoise(seed + 4, Time.time * frequency) * 2) - 1);

        targetObject.transform.localRotation = Quaternion.Euler(new Vector3(randomPerlinRotX, randomPerlinRotY) * shake);

        trauma = Mathf.Clamp01(trauma - recoverySpeed * Time.deltaTime);
    }
    public void InduceTrauma()
    {
        trauma = 1.0f;
        posOfTrauma = targetObject.transform.position;
    }
    public void InduceTrauma(System.Action callback)
    {
        InduceTrauma();
        hasCallBack = true;
        this.callback = callback;
    }
}