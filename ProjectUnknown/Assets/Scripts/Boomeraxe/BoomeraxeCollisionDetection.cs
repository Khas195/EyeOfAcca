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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), other);
            return;
        }
        axe.HandleOnTriggerEnter(other);
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
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
}
