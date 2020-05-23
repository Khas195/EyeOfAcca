using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
    [SerializeField]
    Sprite musicSprite = null;
    [SerializeField]
    Image normalImage = null;
    [SerializeField]
    Sprite mutedMusicSprite = null;

    [SerializeField]
    Sprite soundSprite = null;
    [SerializeField]
    Image normalSoundImage = null;
    [SerializeField]
    Sprite mutedSoundSprite = null;

    bool music = true;
    bool sound = true;
    public void SwitchMusic()
    {
        if (music)
        {
            TurnOffMusic();
            music = false;
        }
        else
        {
            TurnOnMusic();
            music = true;
        }
    }
    public void SwitchSound()
    {
        if (sound)
        {
            TurnOffSound();
            sound = false;
        }
        else
        {
            TurnOnSound();
            sound = true;
        }
    }

    public void TurnOnMusic()
    {
        normalImage.sprite = musicSprite;
        SFXSystem.GetInstance().SwitchMusic(true);
    }
    public void TurnOffMusic()
    {
        normalImage.sprite = mutedMusicSprite;
        SFXSystem.GetInstance().SwitchMusic(false);
    }
    public void TurnOnSound()
    {
        normalSoundImage.sprite = soundSprite;
        SFXSystem.GetInstance().SwitchSounds(true);
    }
    public void TurnOffSound()
    {
        normalSoundImage.sprite = mutedSoundSprite;
        SFXSystem.GetInstance().SwitchSounds(false);
    }
}
