using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class DeadArrowSpawner : MonoBehaviour
{
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    GameObjectPool arrowSpawner = null;

    [BoxGroup("Settings")]
    [SerializeField]
    float spawnInterval = 3f;
    [BoxGroup("Settings")]
    [SerializeField]
    float arrowSpeed = 8f;


    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    Transform startPosition = null;

    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    Transform endPosition = null;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    float currentTime = 0;

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(this.startPosition.position, this.endPosition.position);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(this.startPosition.position, Vector3.one);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.endPosition.position, Vector3.one);
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (currentTime <= 0)
        {
            var arrow = arrowSpawner.RequestInstance();
            var deadArrow = arrow.GetComponent<DeadArrow>();
            deadArrow.SetFlyDirection((endPosition.position - startPosition.position).normalized);
            deadArrow.SetPosition(startPosition.position);
            deadArrow.SetSpeed(arrowSpeed);
            deadArrow.SetCallBackOnHit(OnArrowHit);
            currentTime = spawnInterval;
        }
        currentTime -= Time.deltaTime;
    }
    public void OnArrowHit(GameObject arrow)
    {
        arrowSpawner.ReturnInstance(arrow);
    }
}
