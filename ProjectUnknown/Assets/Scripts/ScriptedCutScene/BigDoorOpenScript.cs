using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class BigDoorOpenScript : MonoBehaviour
{
    [SerializeField]
    [Required]
    Shake cameraShake = null;
    [SerializeField]
    [Required]
    Shake lightShake = null;
    // Start is called before the first frame update
    void Start()
    {
        BigDoorEnd.OnBigDoorOpenStarted.AddListener(OnBigDoorOpen);
        BigDoorEnd.OnSymbolTicked.AddListener(LightShake);
    }
    public void OnBigDoorOpen()
    {
        cameraShake.InduceTrauma();
    }
    public void LightShake()
    {
        lightShake.InduceTrauma();
    }


}
