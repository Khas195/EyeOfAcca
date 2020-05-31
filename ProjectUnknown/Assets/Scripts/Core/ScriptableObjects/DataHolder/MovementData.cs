using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementData", menuName = "Data/Movement", order = 1)]
/**
 * The container for all movement related datas in the game
 * Can be created in the Unity Editor.!-- 
 * Right Click in Folder View -> Data -> movement
 */

[Serializable]
public class MovementData : ScriptableObject
{
    public float bufferTimeForJump = 0.1f;

    public AnimationCurve accelCurve = null;
    public AnimationCurve decelCurve = null;

    public float maxVelUp = 20;
    public float fallMultiplier = 2.5f;
    public float maxJumpHeight = 5f;
    public float jumpHeightForDecel = 3f;
    public float rotateSpeed = 20f;
    public Vector2 currentVelocity;
    [Button("Save")]
    public void SaveData()
    {
        SaveLoadManager.Save(this, this.name);
    }
    [Button("Load")]
    public void LoadData()
    {
        SaveLoadManager.Load(this, this.name);
    }
}
