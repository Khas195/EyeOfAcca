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


    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    float trauma = 0;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    float seed = 0;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    Vector2 posOfTrauma = Vector2.one;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    Quaternion rotOfTrauma = Quaternion.identity;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    bool hasCallBack = false;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    System.Action callback = null;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        seed = UnityEngine.Random.value;
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
            trauma = 0;
            return;
        }

        var shake = Mathf.Pow(trauma, data.traumaExponent);
        // return [0, 1]
        // Then translate it to [-1, 1]
        var randomPerlinX = data.maximumTranslateShake.x * ((Mathf.PerlinNoise(seed, Time.time * data.frequency) * 2) - 1);
        var randomPerlinY = data.maximumTranslateShake.y * ((Mathf.PerlinNoise(seed + 1, Time.time * data.frequency) * 2) - 1);

        Vector3 pos = posOfTrauma + new Vector2(randomPerlinX, randomPerlinY) * shake;
        pos.z = targetObject.transform.position.z;
        targetObject.transform.position = pos;

        var randomPerlinRotX = data.maximumAngularShake.x * ((Mathf.PerlinNoise(seed + 3, Time.time * data.frequency) * 2) - 1);
        var randomPerlinRotY = data.maximumAngularShake.y * ((Mathf.PerlinNoise(seed + 4, Time.time * data.frequency) * 2) - 1);

        Quaternion rot = rotOfTrauma * Quaternion.Euler(new Vector3(randomPerlinRotX, randomPerlinRotY) * shake);
        targetObject.transform.localRotation = rot;

        trauma = Mathf.Clamp01(trauma - data.recoverySpeed * Time.deltaTime);
    }
    public void InduceTrauma()
    {
        trauma = 1.0f;
        posOfTrauma = targetObject.transform.position;
        rotOfTrauma = targetObject.transform.rotation;
    }
    public void InduceTrauma(System.Action callback)
    {
        if (trauma > 0) return;
        InduceTrauma();
        hasCallBack = true;
        this.callback = callback;
    }
}