using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class BoomeraxeCollisionDetection : MonoBehaviour
{
    [SerializeField]
    [Required]
    Boomeraxe axe = null;
    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        axe.HandleCollisionWith(other);
    }
}
