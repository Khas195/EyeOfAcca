using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class BoomeraxeCatcher : MonoBehaviour
{
    [SerializeField]
    [Required]
    BoomeraxeGrip grip = null;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Boomeraxe"))
        {
            grip.HoldAxe();
        }
    }
}
