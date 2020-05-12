using System;
using System.Collections.Generic;
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
        AxeShaking
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

    public List<SFX> resourcesList = new List<SFX>();
}
