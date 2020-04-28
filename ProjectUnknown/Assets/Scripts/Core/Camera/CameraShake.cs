using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    Camera targetCamera = null;
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

        if (trauma <= 0) return;

        var shake = Mathf.Pow(trauma, traumaExponent);
        // return [0, 1]
        // Then translate it to [-1, 1]
        var randomPerlinX = maximumTranslateShake.x * ((Mathf.PerlinNoise(seed, Time.time * frequency) * 2) - 1);
        var randomPerlinY = maximumTranslateShake.y * ((Mathf.PerlinNoise(seed + 1, Time.time * frequency) * 2) - 1);
        var randomPerlinZ = maximumTranslateShake.z * ((Mathf.PerlinNoise(seed + 2, Time.time * frequency) * 2) - 1);

        targetCamera.transform.localPosition = new Vector3(randomPerlinX, randomPerlinY, randomPerlinZ) * shake;

        var randomPerlinRotX = maximumAngularShake.x * ((Mathf.PerlinNoise(seed + 3, Time.time * frequency) * 2) - 1);
        var randomPerlinRotY = maximumAngularShake.y * ((Mathf.PerlinNoise(seed + 4, Time.time * frequency) * 2) - 1);
        var randomPerlinRotZ = maximumAngularShake.z * ((Mathf.PerlinNoise(seed + 5, Time.time * frequency) * 2) - 1);

        targetCamera.transform.localRotation = Quaternion.Euler(new Vector3(randomPerlinRotX, randomPerlinRotY, randomPerlinRotZ) * shake);

        trauma = Mathf.Clamp01(trauma - recoverySpeed * Time.deltaTime);
    }
    public void InduceTrauma()
    {
        trauma = 1.0f;
    }
}