using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Character2D : MonoBehaviour
{
    [SerializeField]
    [BoxGroup("Requirements")]
    [Required]
    Rigidbody2D body = null;
    [SerializeField]
    [BoxGroup("Requirements")]
    [Required]
    IMovement movement = null;

    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    BoomeraxeGrip grip = null;

    [BoxGroup("Settings")]
    [SerializeField]
    [Required]
    MovementData movementWithAxe = null;

    [BoxGroup("Settings")]
    [SerializeField]
    [Required]
    MovementData movementWithouAxe = null;


    // Start is called before the first frame update
    void Start()
    {
        movement.SetRigidBody(body);
        movement.SetMovementData(movementWithAxe);
    }

    public GameObject GetHost()
    {
        return body.gameObject;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (grip.IsHoldingAxe())
        {
            movement.SetMovementData(movementWithAxe);
        }
        else
        {
            movement.SetMovementData(movementWithouAxe);
        }
    }
    public void Jump()
    {
        if (grip.IsHoldingAxe())
        {
            LogHelper.GetInstance().Log(("Axe is so Heavy!!! ").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
        }
        else
        {
            LogHelper.GetInstance().Log(("So light!! ").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
        }
        movement.SignalJump();
    }


    public void Move(float horizontal, float vertical)
    {
        movement.Move(vertical, horizontal);
    }

    public string GetName()
    {
        return name;
    }
}
