using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleCheckPoint : MonoBehaviour
{
    [SerializeField]
    LevelTransitionDoor door = null;

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(door.GetProfile().doorLocation, 1.0f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            GameMaster.GetInstance().UpdateCurrentLevelSettings(door.GetProfile());
        }
    }
}
