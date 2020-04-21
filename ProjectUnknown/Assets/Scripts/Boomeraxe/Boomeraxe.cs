using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Boomeraxe : MonoBehaviour
{
    [SerializeField]
    [Required]
    Rigidbody2D body2d = null;
    [SerializeField]
    float flyDistance = 5f;
    [SerializeField]
    float flyVelocity = 2f;
    [SerializeField]
    [ReadOnly]
    int bounceCount = 0;
    bool flyTriggered = false;
    bool flyingToTarget = false;



    Vector2 originPoint = Vector2.zero;
    Vector2 currentFlyDirection = Vector2.zero;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(originPoint, flyDistance);
    }
    // Start is called before the first frame update
    void Start()
    {
        body2d.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (flyTriggered)
        {
            body2d.velocity = currentFlyDirection * flyVelocity;
            body2d.velocity = Vector2.ClampMagnitude(body2d.velocity, flyVelocity);
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

    public void Fly(Vector2 target)
    {
        LogHelper.GetInstance().Log("Player ".Bolden().Colorize(Color.green) + "has thrown the " + "Boomeraxe".Bolden().Colorize("#83ecd7"), true);
        originPoint = body2d.transform.position;
        currentFlyDirection = (target - originPoint).normalized;
        flyTriggered = true;
        flyingToTarget = true;
    }

    public void Reset()
    {
        currentFlyDirection = Vector3.zero;
        flyTriggered = false;
        bounceCount = 0;
        body2d.velocity = Vector2.zero;
    }

    public int GetBounceCount()
    {
        return bounceCount;
    }

    public void HandleCollisionWith(Collision2D other)
    {
        LogHelper.GetInstance().Log("Boomeraxe".Bolden().Colorize("#83ecd7") + " hit an object");
        bounceCount += 1;
        flyingToTarget = false;
        Reflect(other);
    }

    private void Reflect(Collision2D other)
    {
        Vector2 inDir = currentFlyDirection;
        Vector2 outDir = Vector2.Reflect(currentFlyDirection, other.contacts[0].normal);
        currentFlyDirection = outDir;
        LogHelper.GetInstance().Log("Boomeraxe".Bolden().Colorize("#83ecd7") + " Reflect off object with inDir: " + inDir + " | outDir: " + outDir);
    }
}
