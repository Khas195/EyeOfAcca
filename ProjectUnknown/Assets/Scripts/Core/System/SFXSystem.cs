using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Audio;

public partial class SFXSystem : SingletonMonobehavior<SFXSystem>
{

    [SerializeField]
    SFXResources sFXResources;

    [SerializeField]
    [Required]
    GameObjectPool audioPool = null;

    [SerializeField]
    List<AudioSource> activeSources = new List<AudioSource>();

    [SerializeField]
    AudioSource backgroundMusic = null;
    [SerializeField]
    AudioMixerGroup soundsGroup = null;

    [SerializeField]
    AudioMixerGroup musicGroup = null;
    [SerializeField]
    AudioMixer masterMixer = null;

    public void PlayBGMusic()
    {
        backgroundMusic.outputAudioMixerGroup = musicGroup;
        backgroundMusic.Stop();
        backgroundMusic.Play();
    }
    public void StopBGMusic()
    {
        backgroundMusic.Stop();
    }



    public bool IsBGMusicPlaying()
    {
        return backgroundMusic.isPlaying;
    }

    public AudioSource PlaySound(SFXResources.SFXList soundsEnum)
    {
        var sourceObj = audioPool.RequestInstance();
        var source = sourceObj.GetComponent<AudioSource>();
        activeSources.Add(source);
        PlaySound(soundsEnum, source);

        return source;
    }

    public void PlaySound(SFXResources.SFXList soundsEnum, AudioSource source)
    {
        for (int i = 0; i < sFXResources.resourcesList.Count; i++)
        {
            var sfx = sFXResources.resourcesList[i];
            if (sfx.tag.Equals(soundsEnum))
            {
                if (sfx.clip == null)
                {
                    break;
                }
                source.clip = sfx.clip;
                source.loop = sfx.loop;
                source.volume = sfx.volumn;
                source.outputAudioMixerGroup = soundsGroup;
                break;
            }
        }
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        TurnOffInactiveSource();
    }

    private void TurnOffInactiveSource()
    {
        for (int i = 0; i < activeSources.Count; i++)
        {
            if (activeSources[i].isPlaying == false)
            {
                LogHelper.GetInstance().Log(("Audio " + activeSources[i].clip + " has stop playing").Bolden(), true, LogHelper.LogLayer.Console);
                activeSources[i].clip = null;
                audioPool.ReturnInstance(activeSources[i].gameObject);
                activeSources.RemoveAt(i);
            }
        }
    }

    public void StopAllSounds()
    {
        LogHelper.GetInstance().Log(("STOP ALL Audios ").Bolden(), true);
        for (int i = 0; i < activeSources.Count; i++)
        {
            activeSources[i].Stop();
            activeSources[i].clip = null;
            audioPool.ReturnInstance(activeSources[i].gameObject);
            activeSources.RemoveAt(i);
        }
    }
    public void SwitchMusic(bool on)
    {
        if (on)
        {
            this.masterMixer.SetFloat("musicVol", -21.0f);
        }
        else
        {
            this.masterMixer.SetFloat("musicVol", -80.0f);
        }
    }
    public void SwitchSounds(bool on)
    {
        if (on)
        {
            this.masterMixer.SetFloat("sfxVol", -21.0f);
        }
        else
        {
            this.masterMixer.SetFloat("sfxVol", -80.0f);
        }
    }
}
