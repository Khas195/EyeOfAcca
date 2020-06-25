using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadCountUpdate : MonoBehaviour
{
    [SerializeField]
    LevelSettings curSettings = null;
    [SerializeField]
    Text deadCount = null;

    // Update is called once per frame
    void FixedUpdate()
    {
        deadCount.text = curSettings.DeadCount.ToString();
    }
}
