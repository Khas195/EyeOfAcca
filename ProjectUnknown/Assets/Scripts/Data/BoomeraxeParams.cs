﻿using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "BoomeraxeDatas", menuName = "Data/Boomeraxe", order = 1)]
public class BoomeraxeParams : ScriptableObject
{
    public float flyDistance = 5f;
    public float accelration = 2.0f;
    public float flyVelocity = 2f;
    public int maxBounce = 2;
    public int maxTeleport = 2;
    public float lulTimeAfterTeleport = 0.2f;
    public float timeScaleAfterTeleport = 0.2f;
    public float lulPeriodAfterAirborneThrow = 0.2f;
    public float timeScaleAfterThrow = 0.2f;
    public float timeTilAxeCatchable = 0.2f;
    public float timeTilAxeReturnAfterExitCameraView = 0.5f;

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
