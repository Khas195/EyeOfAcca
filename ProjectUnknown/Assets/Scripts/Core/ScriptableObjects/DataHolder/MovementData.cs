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

    [OnValueChanged("CalcualteInitialVelocityAndGravity")]
    public AnimationCurve jumpCurve = null;
    public void CalcualteInitialVelocityAndGravity(AnimationCurve oldValue, AnimationCurve newValue)
    {
        var maxHeight = 0.0f;
        var timeToReachPeak = 0.0f;
        for (int i = 0; i < jumpCurve.keys.Length; i++)
        {
            if (maxHeight < jumpCurve.keys[i].value)
            {
                maxHeight = jumpCurve.keys[i].value;
                timeToReachPeak = jumpCurve.keys[i].time;
            }
        }
        initialJumpVelocity = (2 * maxHeight) / timeToReachPeak;
        jumpGravity = -initialJumpVelocity / timeToReachPeak;
        fallGravity = -(2 * maxHeight) / Mathf.Pow(jumpCurve.keys[jumpCurve.keys.Length - 1].time - timeToReachPeak, 2);
    }

    public float initialJumpVelocity = 0;
    public float jumpGravity = 0;
    public float fallGravity = 0;
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
