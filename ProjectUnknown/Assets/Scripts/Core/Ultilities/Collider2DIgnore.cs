using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Collider2DIgnore : MonoBehaviour
{
    [SerializeField]
    [Tag]
    string ignoreTag = "";

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag.Equals(ignoreTag))
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collisionInfo.collider);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals(ignoreTag))
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), other);
        }
    }
}
