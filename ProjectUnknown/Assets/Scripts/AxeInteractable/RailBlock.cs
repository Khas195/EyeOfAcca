using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

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

    [BoxGroup("Settings")]
    [SerializeField]
    float timeTillDestinationReached = 1f;

    [BoxGroup("Settings")]
    [SerializeField]
    int travelBoxWidth = 2;

    [BoxGroup("Settings")]
    [SerializeField]
    bool travelHorizontal = true;

    Transform holderTrans = null;

    Vector3 originPos = Vector3.one;

    [BoxGroup("Status")]
    [SerializeField]
    [ReadOnly]
    Vector3 blockLeftContraint;

    [BoxGroup("Status")]
    [SerializeField]
    [ReadOnly]
    Vector3 blockRightContraint;

    [BoxGroup("Status")]
    [SerializeField]
    [ReadOnly]
    bool inMotion = false;

    [BoxGroup("Status")]
    [SerializeField]
    [ReadOnly]
    Vector3 destination;
    [BoxGroup("Status")]
    [SerializeField]
    [ReadOnly]
    float currentTravelTime = 0;

    [BoxGroup("Status")]
    [SerializeField]
    [ReadOnly]
    int axeHitSide = 0;

    [BoxGroup("Status")]
    [SerializeField]
    [ReadOnly]
    Boomeraxe axe = null;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(originPos, blockLeftContraint);
        Gizmos.DrawWireCube(blockLeftContraint, Vector3.one * box.bounds.extents.x * 2);
        Gizmos.DrawLine(originPos, blockRightContraint);
        Gizmos.DrawWireCube(blockRightContraint, Vector3.one * box.bounds.extents.x * 2);
        Gizmos.color = Color.red;
    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        CalculateContraint();
    }

    [Button("Calculate Constraint")]
    private void CalculateContraint()
    {
        originPos = this.transform.position;
        var boxWidth = box.bounds.extents.x * 2;
        blockLeftContraint = originPos - boxWidth * travelBoxWidth * this.transform.right;
        blockRightContraint = originPos + boxWidth * travelBoxWidth * this.transform.right;

    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (inMotion)
        {
            this.transform.position = Tweener.EaseOutQuad(currentTravelTime, originPos, destination, timeTillDestinationReached);
            currentTravelTime += Time.deltaTime;
            if (Vector2.Distance(this.transform.position, destination) <= 0.01f)
            {
                inMotion = false;
                currentTravelTime = 0;
            }
        }
    }

    public override void OnAxeHit(Boomeraxe axe)
    {
        base.OnAxeHit(axe);
        holderTrans = axe.GetHolder();
        var axePos = axe.GetAxePosition();
        this.axe = axe;
        if (travelHorizontal)
        {
            if (axePos.x > this.transform.position.x)
            {
                axeHitSide = 1;
            }
            else if (axePos.x < this.transform.position.x)
            {
                axeHitSide = -1;
            }

        }
        else
        {
            if (axePos.y > this.transform.position.y)
            {
                axeHitSide = 1;
            }
            else if (axePos.y < this.transform.position.y)
            {
                axeHitSide = -1;
            }
        }
    }

    public override void OnAxeAbilityTriggered(AxeAbility triggeredAbility)
    {
        base.OnAxeAbilityTriggered(triggeredAbility);
        if (abilityToInteract.Equals(triggeredAbility) && axe.GetStuckCollider() == box)
        {
            if (travelHorizontal)
            {
                if (holderTrans.position.x > this.transform.position.x && axeHitSide == 1)
                {
                    GoTo(blockRightContraint);
                }
                else if (holderTrans.position.x < this.transform.position.x && axeHitSide == -1)
                {
                    GoTo(blockLeftContraint);
                }

            }
            else
            {
                if (holderTrans.position.y > this.transform.position.y && axeHitSide == 1)
                {
                    GoTo(blockRightContraint);
                }
                else if (holderTrans.position.y < this.transform.position.y && axeHitSide == -1)
                {
                    GoTo(blockLeftContraint);
                }
            }
        }
    }

    private void GoTo(Vector3 targetPos)
    {
        this.destination = targetPos;
        this.originPos = this.transform.position;
        inMotion = true;
    }
}
