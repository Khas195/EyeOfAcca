using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationSoundFeedback : MonoBehaviour
{
    AudioSource walkSource = null;
    public void PlayWalkSound()
    {
        walkSource = SFXSystem.GetInstance().PlaySound(SFXResources.SFXList.footStep_Walk);
    }
    public void StopWalkSound()
    {
        if (walkSource != null)
        {
            walkSource.Stop();
            walkSource = null;

        }
    }
}