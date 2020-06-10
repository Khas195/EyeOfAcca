using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Level : MonoBehaviour
{
    [SerializeField]
    [Required]
    Tilemap groundTileMap = null;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        var gameMaster = GameMaster.GetInstance();
        if (gameMaster)
        {
            gameMaster.UpdateLevelSettingsBounds(this.GetGroundMapPosition(), this.GetGroundMapBounds());
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundTileMap.transform.position + groundTileMap.cellBounds.center, groundTileMap.cellBounds.size);
    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>

    public Vector2 GetGroundMapPosition()
    {
        return groundTileMap.transform.position + groundTileMap.cellBounds.center;
    }

    public Vector3 GetGroundMapBounds()
    {
        return groundTileMap.cellBounds.size;
    }
}
