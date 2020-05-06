using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

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


    public void PlayBGMusic()
    {
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

        for (int i = 0; i < sFXResources.resourcesList.Count; i++)
        {
            var sfx = sFXResources.resourcesList[i];
            if (sfx.tag.Equals(soundsEnum))
            {
                source.clip = sfx.clip;
                source.loop = sfx.loop;
                source.volume = sfx.volumn;
                source.Play();
                return source;
            }
        }
        return null;
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
                LogHelper.GetInstance().Log(("Audio " + activeSources[i].clip + " has stop playing").Bolden(), true);
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
}
