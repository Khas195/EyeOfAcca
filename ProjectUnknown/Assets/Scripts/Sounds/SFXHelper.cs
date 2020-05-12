using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXHelper : MonoBehaviour
{
    [SerializeField]
    SFXResources.SFXList soundToPlay;
    AudioSource sound = null;
    public void PlaySound()
    {
        sound = SFXSystem.GetInstance().PlaySound(soundToPlay);
    }
    public void StopSound()
    {
        if (sound)
        {
            sound.Stop();
        }
    }
}
