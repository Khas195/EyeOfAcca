using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "SFXs", menuName = "SFX", order = 0)]
public class SFXResources : ScriptableObject
{
    public enum SFXList
    {
        footStep_Run,
        footStep_Walk,
        playerJump,
        axeSpinning,
        axeHit,
        axeThrow,
        axeShake,
        playerLand,
        RaillMoving,
        Teleport,
        AxeShaking,
        PotBreak,
        DoorFalling,
        DoorClosed,
        OnRailMove,
        RailBlockStop
    }
    [Serializable]
    public struct SFX
    {
        public SFXList tag;
        public AudioClip clip;
        public bool loop;
        [Range(0, 1)]
        public float volumn;
    }

    [ReorderableList]
    public List<SFX> resourcesList = new List<SFX>();


#if UNITY_EDITOR

    [BoxGroup("Preview")]
    [SerializeField]
    bool reviewClip = false;
    [BoxGroup("Preview")]
    [SerializeField]
    [HideIf("reviewClip")]
    SFXList soundToPreview = SFXList.footStep_Run;
    [BoxGroup("Preview")]
    [SerializeField]
    [ShowIf("reviewClip")]
    AudioClip previewClip = null;
    AudioSource previewer = null;
    [Button]
    public void Preview()
    {
        if (previewer == null)
        {
            previewer = UnityEditor.EditorUtility.CreateGameObjectWithHideFlags("Audio Preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
        }

        if (reviewClip == false)
        {
            var sound = resourcesList.Find(x => (x.tag.Equals(soundToPreview)));
            previewer.volume = sound.volumn;
            previewer.clip = sound.clip;
        }
        else
        {
            previewer.clip = previewClip;
        }
        previewer.Play();
    }

#endif
}
