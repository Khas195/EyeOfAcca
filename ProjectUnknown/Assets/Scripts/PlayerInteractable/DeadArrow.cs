using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class DeadArrow : MonoBehaviour
{

    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    Rigidbody2D body = null;


    [BoxGroup("Settings")]
    [SerializeField]
    float speed = 2.0f;

    [BoxGroup("Optional")]
    [SerializeField]
    Transform arrowBreakPlace = null;



    [BoxGroup("Status")]
    [SerializeField]
    [ReadOnly]
    Vector2 flyDirection;

    [BoxGroup("Status")]
    [SerializeField]
    [ReadOnly]

    Action<GameObject> onHit;



    public void SetFlyDirection(Vector3 newDirection)
    {
        this.flyDirection = newDirection;
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        body.velocity = flyDirection * this.speed;
        var lookRot = Quaternion.LookRotation(Vector3.forward, body.velocity.normalized);
        body.transform.localRotation = lookRot;
    }

    public void SetPosition(Vector3 position)
    {
        body.transform.position = position;
    }

    public void SetCallBackOnHit(Action<GameObject> onArrowHit)
    {
        this.onHit = onArrowHit;
    }
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (arrowBreakPlace != null)
        {
            var effect = VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.ArrowBreaks, this.arrowBreakPlace.position, Quaternion.identity);
            effect.transform.rotation = Quaternion.LookRotation(flyDirection);
        }

        this.onHit(body.gameObject);
        if (collisionInfo.collider.tag.Equals("Player"))
        {
            LogHelper.GetInstance().Log(("Player hit Player").Bolden(), true, LogHelper.LogLayer.PlayerFriendly);
            var chip = collisionInfo.collider.GetComponent<Chip>();
            if (chip)
            {
                chip.InitiateDeadSequence();
            }
        }
    }

    public void SetSpeed(float arrowSpeed)
    {
        this.speed = arrowSpeed;
    }
}

