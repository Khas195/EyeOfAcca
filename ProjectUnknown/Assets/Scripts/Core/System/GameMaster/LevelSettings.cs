using System;
using NaughtyAttributes;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "LevelSettings", menuName = "Data/LevelSettings", order = 1)]
public class LevelSettings : ScriptableObject
{
    [SerializeField]
    public TransitionDoorProfile startSpawn = null;
    [SerializeField]
    public TransitionDoorProfile currentSpawn = null;
    [ReadOnly]
    public string currentLevel = "";

    [ReadOnly]
    public Vector3 levelCenter = Vector3.zero;
    [ReadOnly]
    public Vector3 levelBounds = Vector3.zero;

}
