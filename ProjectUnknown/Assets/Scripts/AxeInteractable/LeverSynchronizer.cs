using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class LeverSynchronizer : MonoBehaviour
{
    [SerializeField]
    [ReadOnly]
    List<RailBlock> levers = new List<RailBlock>();

    [SerializeField]
    [ReadOnly]
    List<Door> doors = new List<Door>();
    [SerializeField]
    float moveTime = 10f;
    [SerializeField]
    float returnTime = 5f;
    void Start()
    {
        FindRailsInChildren();
        FindDoorsInChildren();
        SyncMoveTime();
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        for (int i = 0; i < levers.Count; i++)
        {
            Gizmos.DrawWireSphere(levers[i].transform.position, 1f);
            Gizmos.DrawLine(levers[i].transform.position, this.transform.position);
        }
        for (int i = 0; i < doors.Count; i++)
        {
            Gizmos.DrawWireSphere(doors[i].transform.position, 1f);
            Gizmos.DrawLine(doors[i].transform.position, this.transform.position);
        }
        Gizmos.DrawWireSphere(this.transform.position, 1f);

    }

    [Button]
    private void SyncMoveTime()
    {
        var move = this.GetComponentsInChildren<MoveAB>();
        for (int i = 0; i < move.Length; i++)
        {
            move[i].SetMoveTime(moveTime);
            move[i].SetReturnTime(returnTime);
        }
    }

    public void OnLeverMove()
    {
        for (int i = 0; i < doors.Count; i++)
        {
            if (doors[i].CheckIsOpen())
            {
                doors[i].Close();
            }
            else
            {
                doors[i].Open();
            }
        }
        for (int i = 0; i < levers.Count; i++)
        {
            levers[i].Move();
        }
    }

    [Button]
    private void FindDoorsInChildren()
    {
        doors.Clear();
        var foundDoors = this.GetComponentsInChildren<Door>();
        doors.AddRange(foundDoors);
    }

    [Button]
    private void FindRailsInChildren()
    {
        levers.Clear();
        var rails = this.GetComponentsInChildren<RailBlock>();
        for (int i = 0; i < rails.Length; i++)
        {
            rails[i].OnBlockMove.AddListener(OnLeverMove);
        }
        levers.AddRange(rails);
    }

    void Update()
    {

    }
}
