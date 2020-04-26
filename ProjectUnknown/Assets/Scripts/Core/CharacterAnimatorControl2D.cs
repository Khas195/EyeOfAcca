using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class CharacterAnimatorControl2D : MonoBehaviour
{
    [SerializeField]
    [Required]
    Rigidbody2D body2d = null;
    [SerializeField]
    [Required]
    Animator anim = null;

    // Update is called once per frame
    void Update()
    {
        if (body2d.velocity.x != 0)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }
}
