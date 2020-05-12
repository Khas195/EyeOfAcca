using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXHelper : MonoBehaviour
{
    [SerializeField]
    SFXResources.SFXList soundToPlay;
    public void PlaySound()
    {
        SFXSystem.GetInstance().PlaySound(soundToPlay);
    }
}
