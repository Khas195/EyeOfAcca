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

    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    GameMasterSettings settings = null;

    [BoxGroup("Settings")]
    [SerializeField]
    public UnityEvent OnBlockMove = new UnityEvent();

    [BoxGroup("Settings")]
    [SerializeField]
    public UnityEvent OnStartedMoving = new UnityEvent();



    [BoxGroup("Settings")]
    [SerializeField]
    public UnityEvent OnBlockMoveA = new UnityEvent();
    [BoxGroup("Settings")]
    [SerializeField]
    public UnityEvent OnBlockMoveB = new UnityEvent();
    [BoxGroup("Settings")]
    [SerializeField]
    bool isRailBlock = true;






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
        if (isRailBlock)
        {
            if (settings.RailUnlocked == false) return;
        }

        base.OnAxeHit(axe);
        holderTrans = axe.GetHolder();
        var axePos = axe.GetAxePosition();
        this.axe = axe;
    }

    public override void OnAxeAbilityTriggered(AxeAbility triggeredAbility)
    {
        if (isRailBlock)
        {
            if (settings.RailUnlocked == false) return;
        }

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
