using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class CharacterAnimatorControl2D : MonoBehaviour
{
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    Rigidbody2D body2d = null;

    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    Animator anim = null;

    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    BoomeraxeGrip grip = null;

    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    Movement2DPlatform characterMovement = null;

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(characterMovement.GetDesiredMovementHorizontal()));
        anim.SetFloat("VelocityVertical", body2d.velocity.y);
        anim.SetBool("IsHoldingAxe", grip.GetIsHoldingAxe());
        anim.SetBool("IsTouchingGround", characterMovement.IsTouchingGround());
        anim.SetBool("Jump", characterMovement.GetJumpTriggered());
    }
    public void ThrowCatchEvent(bool isThrow)
    {
        if (isThrow)
        {
            anim.SetTrigger("throwAxeTrigger");
        }
        else
        {

            anim.SetTrigger("axeCatchTrigger");
        }
    }
    public void PlayThrowAnimation()
    {
        ThrowCatchEvent(true);
    }
}
