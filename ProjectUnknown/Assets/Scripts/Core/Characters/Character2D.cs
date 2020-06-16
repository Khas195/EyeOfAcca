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

    public bool TryDropDown()
    {
        LogHelper.GetInstance().Log("Try drop down".Bolden(), true, LogHelper.LogLayer.PlayerFriendly);
        var cols = movement.GetGroundCollider2D();
        if (cols == null) return false;
        for (int i = 0; i < cols.Length; i++)
        {
            var col = cols[i];
            if (col.tag.Equals("Oneway-Platform"))
            {
                Physics2D.IgnoreCollision(col, body.GetComponent<Collider2D>());
                StartCoroutine(EnableCollision(0.3f, col));
                return true;
            }
        }

        return false;
    }

    IEnumerator EnableCollision(float delay, Collider2D platform)
    {
        yield return new WaitForSeconds(delay);
        Physics2D.IgnoreCollision(platform, body.GetComponent<Collider2D>(), false);
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
    public bool TryJump()
    {
        if (movement.CanJump())
        {
            movement.SignalJump();
            return true;
        }
        return false;
    }


    public void Move(float horizontal, float vertical)
    {
        movement.Move(vertical, horizontal);
    }

    public string GetName()
    {
        return name;
    }

    public void SetPosition(Vector3 landingPosition)
    {
        this.body.transform.position = landingPosition;
    }

    public void StartFalling()
    {
        movement.StartFalling();
    }
}
