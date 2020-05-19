using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class RailBlock : AxeInteractable
{


    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    AxeAbility abilityToInteract = null;

    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    BoxCollider2D box = null;

    [BoxGroup("Requirements")]
    [SerializeField]
    bool moveHorizontal = true;

    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    MoveAB moveAb = null;

    [BoxGroup("Settings")]
    [SerializeField]
    UnityEvent OnBlockMove = new UnityEvent();

    [BoxGroup("Settings")]
    [SerializeField]
    UnityEvent OnBlockMoveA = new UnityEvent();
    [BoxGroup("Settings")]
    [SerializeField]
    UnityEvent OnBlockMoveB = new UnityEvent();





    [BoxGroup("Status")]
    [SerializeField]
    [ReadOnly]
    Transform holderTrans = null;
    [BoxGroup("Status")]
    [SerializeField]
    [ReadOnly]
    Boomeraxe axe = null;

    public override void OnAxeHit(Boomeraxe axe)
    {
        base.OnAxeHit(axe);
        holderTrans = axe.GetHolder();
        var axePos = axe.GetAxePosition();
        this.axe = axe;
    }

    public override void OnAxeAbilityTriggered(AxeAbility triggeredAbility)
    {
        base.OnAxeAbilityTriggered(triggeredAbility);
        bool moveA = true;
        if (abilityToInteract.Equals(triggeredAbility) && axe.GetStuckCollider() == box)
        {
            if (moveHorizontal)
            {
                if (holderTrans.transform.position.x > box.transform.position.x)
                {
                    moveAb.GoTo(MoveAB.MoveABEnum.B);
                    moveA = false;
                }
                else
                {
                    moveAb.GoTo(MoveAB.MoveABEnum.A);
                }
            }
            else
            {
                if (holderTrans.transform.position.y >= box.transform.position.y)
                {
                    moveAb.GoTo(MoveAB.MoveABEnum.B);
                    moveA = false;
                }
                else
                {
                    moveAb.GoTo(MoveAB.MoveABEnum.A);
                }

            }
            if (moveAb.HasReachedDestination() == false)
            {
                if (moveA)
                {
                    OnBlockMoveA.Invoke();
                }
                else
                {
                    OnBlockMoveB.Invoke();
                }

                OnBlockMove.Invoke();
            }
        }
    }
}
