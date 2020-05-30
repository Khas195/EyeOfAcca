using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class SFXHelper : MonoBehaviour
{
    [SerializeField]
    SFXResources.SFXList soundToPlay;
    [SerializeField]
    bool manualAudioSource = false;
    [SerializeField]
    [ShowIf("manualAudioSource")]
    [Required]
    AudioSource sound = null;
    public void PlaySound()
    {
        if (manualAudioSource)
        {
            SFXSystem.GetInstance().PlaySound(soundToPlay, sound);
        }
        else
        {
            sound = SFXSystem.GetInstance().PlaySound(soundToPlay);
        }
    }
    public void StopSound()
    {
        if (sound)
        {
            sound.Stop();
        }
    }
}
