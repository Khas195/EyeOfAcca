using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;


[CreateAssetMenu(fileName = "VFXs", menuName = "VFX", order = 0)]
public class VFXResources : ScriptableObject
{
    public enum VFXList
    {
        Heal,
        PlayerLand,
        FootFall,
        Pebbles,
        Teleport,
        OrbGatherPower,
        AxeHasPowerFlash,
        OnTeleGemHit,
        RailBlockParticles,
        DoorCloseDust,
        AxeRecalling,
        PotBreak,
        TrapFireSmoke,
        ArrowBreaks,
        CharacterStoneBreaks,
        GemUnlock,
        RailUnlock,
        TimedDoorUnlock
    }
    [Serializable]
    public struct VFX
    {
        public VFXList tag;
        public GameObject prefab;
    }
    public List<VFX> resourcesList = new List<VFX>();
}
