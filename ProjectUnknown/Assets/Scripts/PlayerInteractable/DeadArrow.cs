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
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        Vector2 currentPos = body.transform.position;
        body.MovePosition(currentPos + flyDirection * this.speed * Time.deltaTime);
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
        this.onHit(body.gameObject);
        if (collisionInfo.collider.tag.Equals("Player"))
        {
            SFXSystem.GetInstance().StopAllSounds();
            LogHelper.GetInstance().Log(("Player hit Player").Bolden(), true, LogHelper.LogLayer.PlayerFriendly);
            GameMaster.GetInstance().LoadLevel(GameMaster.GetInstance().GetStartLevel());
        }
    }

    public void SetSpeed(float arrowSpeed)
    {
        this.speed = arrowSpeed;
    }
}

