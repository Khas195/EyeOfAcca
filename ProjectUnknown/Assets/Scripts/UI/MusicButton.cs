using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
    [SerializeField]
    Sprite musicSprite;
    [SerializeField]
    Image normalImage;
    [SerializeField]
    Sprite mutedMusicSprite;

    [SerializeField]
    Sprite soundSprite;
    [SerializeField]
    Image normalSoundImage;
    [SerializeField]
    Sprite mutedSoundSprite;

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
