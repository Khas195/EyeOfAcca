using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Boomeraxe : MonoBehaviour
{
    [SerializeField]
    [Required]
    Camera playerCamera = null;
    [SerializeField]
    [Required]
    GameObject boomeraxeHolder = null;
    [SerializeField]
    [Required]
    Rigidbody2D body2d = null;
    [SerializeField]
    float flyDistance = 5f;
    [SerializeField]
    float flyVelocity = 2f;
    bool flyTriggered = false;
    bool flyingToTarget = false;



    Vector3 originPoint = Vector3.zero;
    Vector3 currentFlyDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        body2d.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var mousPos = Input.mousePosition;
        mousPos = playerCamera.ScreenToWorldPoint(mousPos);

        if (Input.GetMouseButtonDown(0) && flyTriggered == false)
        {
            Throw(mousPos);
        }
        if (flyTriggered)
        {
            body2d.velocity = currentFlyDirection.normalized * flyVelocity;
            if (flyingToTarget)
            {
                if (Vector2.Distance(originPoint, body2d.transform.position) > flyDistance)
                {
                    LogHelper.GetInstance().Log("Boomeraxe".Bolden().Colorize("#83ecd7") + " has reached max flying distance, now returning", true);
                    flyingToTarget = false;
                    currentFlyDirection *= -1;
                }
            }
        }
    }

    private void Throw(Vector3 target)
    {
        LogHelper.GetInstance().Log("Player ".Bolden().Colorize(Color.green) + "has thrown the " + "Boomeraxe".Bolden().Colorize("#83ecd7"), true);
        originPoint = body2d.transform.position;
        currentFlyDirection = (target - originPoint).normalized;
        flyTriggered = true;
        flyingToTarget = true;
        body2d.gravityScale = 0;
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        LogHelper.GetInstance().Log("Boomeraxe".Bolden().Colorize("#83ecd7") + " hit an object", true);
        body2d.gravityScale = 1.0f;
        currentFlyDirection = Vector3.zero;
        flyTriggered = false;
    }

    public void HandleCollisionWith(Collision2D other)
    {
        LogHelper.GetInstance().Log("Boomeraxe".Bolden().Colorize("#83ecd7") + " hit an object", true);
        body2d.gravityScale = 1.0f;
        currentFlyDirection = Vector3.zero;
        flyTriggered = false;
    }
}
