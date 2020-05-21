using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[Serializable]
public class LevelCheckPoint
{
    public Transform spawnTrans = null;
    public Transform checkPointTriggerTrans = null;
    public Vector2 checkPointTriggerSize = Vector2.one;
}
public class Level : SingletonMonobehavior<Level>
{
    [SerializeField]
    List<LevelCheckPoint> checkpoints = new List<LevelCheckPoint>();

    [SerializeField]
    [ReadOnly]
    LevelCheckPoint current = null;
    [SerializeField]
    List<LevelTransitionDoor> doors = new List<LevelTransitionDoor>();
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (var item in checkpoints)
        {
            Gizmos.DrawLine(item.spawnTrans.position, item.checkPointTriggerTrans.position);
            Gizmos.DrawWireSphere(item.spawnTrans.position, 1.0f);
            Gizmos.DrawWireCube(item.checkPointTriggerTrans.position, item.checkPointTriggerSize);
        }
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        for (int i = 0; i < checkpoints.Count; i++)
        {
            Collider2D[] result = Physics2D.OverlapBoxAll(checkpoints[i].checkPointTriggerTrans.position, checkpoints[i].checkPointTriggerSize, 0);
            if (result.Length > 0)
            {
                foreach (var col in result)
                {
                    if (col != null)
                    {
                        if (col.tag.Equals("Player"))
                        {
                            current = checkpoints[i];
                            if (current != null)
                            {
                                GameMaster.GetInstance().SetSpawnPoint(current.spawnTrans.position);
                            }
                        }
                    }
                }
            }
        }

    }

    public GameObject GetDoor(int doorIndex)
    {
        return this.doors[doorIndex].gameObject;
    }
}
