using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class BoomeraxeCollisionDetection : MonoBehaviour
{
    [SerializeField]
    [Required]
    Boomeraxe axe = null;
    [SerializeField]
    [Required]
    Collider2D col = null;



    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log(other);
        if (other.gameObject.tag.Equals("Player"))
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), other.collider);
            return;
        }
        axe.HandleCollision(other);
    }
    void FixedUpdate()
    {
        CheckForOverlap();
    }

    private void CheckForOverlap()
    {
        Collider2D[] result = new Collider2D[1];
        int numOfCOl = col.OverlapCollider(new ContactFilter2D(), result);
        if (numOfCOl > 0)
        {
            foreach (var col in result)
            {
                if (col != null)
                {
                    if (col.tag.Equals("Player"))
                    {
                        axe.OnCollideWithHolder();
                    }
                }
            }
        }
    }
    public Boomeraxe GetAxe()
    {
        return axe;
    }
}
