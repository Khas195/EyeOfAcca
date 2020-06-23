using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "BoomeraxeDatas", menuName = "Data/Boomeraxe", order = 1)]
public class BoomeraxeParams : ScriptableObject
{
    public float flyVelocity = 2f;
    [OnValueChanged("CalculateRecallTime")]
    public AnimationCurve recallCurve = null;
    public AnimationCurve recallTimeBaseOnDistance = null;
    public int maxTeleport = 2;
    public float lulTimeAfterTeleport = 0.2f;
    public float timeScaleAfterTeleport = 0.2f;
    public float timeScaleOnAxeRecall = 0.2f;
    public float timeTilAxeCatchable = 0.2f;
    [SerializeField]
    [ReadOnly]
    public float recallTime = 0;

    public void CalculateRecallTime()
    {
        recallTime = recallCurve.keys[recallCurve.keys.Length - 1].time;
    }

    public float teleportDistanceAwayFromDestination = 0.2f;

    [Button("Save")]
    public void Save()
    {
        this.SaveData();
    }
    [Button("Load")]
    public void Load()
    {
        this.LoadData();
    }
}
