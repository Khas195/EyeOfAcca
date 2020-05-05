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
}
