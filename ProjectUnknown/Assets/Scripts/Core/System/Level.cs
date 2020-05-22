using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;


public class Level : SingletonMonobehavior<Level>
{
    [SerializeField]
    [ReadOnly]
    List<LevelTransitionDoor> doors = new List<LevelTransitionDoor>();

    [SerializeField]
    [Required]
    GameObject doorsParent = null;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        doors.AddRange(doorsParent.GetComponentsInChildren<LevelTransitionDoor>());
    }
    public GameObject GetDoor(int doorIndex)
    {
        return this.doors[doorIndex].gameObject;
    }

    public void AddDoor(LevelTransitionDoor levelTransitionDoor)
    {
        doors.Add(levelTransitionDoor);
    }
}
