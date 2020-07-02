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
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    [ShowIf("isRailBlock")]
    SpriteRenderer render = null;
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    [ShowIf("isRailBlock")]
    Sprite normalBlock = null;
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    [ShowIf("isRailBlock")]
    Sprite lockedBlock = null;





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

    void Start()
    {
        if (isRailBlock)
        {
            UpdateRailBlockSprite();
            settings.OnRailUnlocked.AddListener(OnRailUnlocked);
        }
    }

    private void UpdateRailBlockSprite()
    {
        if (settings.RailUnlocked)
        {
            render.sprite = normalBlock;
        }
        else
        {
            render.sprite = lockedBlock;
        }
    }

    private void OnRailUnlocked()
    {
        UpdateRailBlockSprite();
        VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.RailUnlock, this.transform.position, Quaternion.identity);
    }

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
        if (abilityToInteract.Equals(triggeredAbility) && axe.GetStuckCollider() == box)
        {
            if (moveHorizontal)
            {
                if (holderTrans.transform.position.x > box.transform.position.x)
                {
                    MoveRight();
                }
                else
                {
                    MoveLeft();
                }
            }
            else
            {
                if (holderTrans.transform.position.y >= box.transform.position.y)
                {
                    MoveUp();
                }
                else
                {
                    MoveDown();
                }
            }
            if (moveAb.HasReachedDestination() == false)
            {
                if (moveAb.IsCurrentDestination(MoveAB.MoveABEnum.A))
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
    public void Move()
    {
        if (moveAb.IsAt(MoveAB.MoveABEnum.A))
        {
            moveAb.GoTo(MoveAB.MoveABEnum.B);
        }
        else
        {
            moveAb.GoTo(MoveAB.MoveABEnum.A);
        }
    }
    private void MoveDown()
    {
        var posA = moveAb.GetPosition(MoveAB.MoveABEnum.A);
        var posB = moveAb.GetPosition(MoveAB.MoveABEnum.B);

        if (IsPostionAAbovePositionB(posA, posB))
        {
            moveAb.GoTo(MoveAB.MoveABEnum.B);
        }
        else
        {
            moveAb.GoTo(MoveAB.MoveABEnum.A);
        }
    }

    private static bool IsPostionAAbovePositionB(Vector2 posA, Vector2 posB)
    {
        return posA.y > posB.y;
    }

    private void MoveUp()
    {
        var posA = moveAb.GetPosition(MoveAB.MoveABEnum.A);
        var posB = moveAb.GetPosition(MoveAB.MoveABEnum.B);

        if (IsPostionAAbovePositionB(posA, posB))
        {
            moveAb.GoTo(MoveAB.MoveABEnum.A);
        }
        else
        {
            moveAb.GoTo(MoveAB.MoveABEnum.B);
        }
    }

    private void MoveLeft()
    {
        var posA = moveAb.GetPosition(MoveAB.MoveABEnum.A);
        var posB = moveAb.GetPosition(MoveAB.MoveABEnum.B);

        if (IsPositionARightOfPositionB(posA, posB))
        {
            moveAb.GoTo(MoveAB.MoveABEnum.B);
        }
        else
        {
            moveAb.GoTo(MoveAB.MoveABEnum.A);
        }
    }

    private static bool IsPositionARightOfPositionB(Vector2 posA, Vector2 posB)
    {
        return posA.x > posB.x;
    }

    private void MoveRight()
    {
        var posA = moveAb.GetPosition(MoveAB.MoveABEnum.A);
        var posB = moveAb.GetPosition(MoveAB.MoveABEnum.B);

        if (IsPositionARightOfPositionB(posA, posB))
        {
            moveAb.GoTo(MoveAB.MoveABEnum.A);
        }
        else
        {
            moveAb.GoTo(MoveAB.MoveABEnum.B);
        }

    }
}
