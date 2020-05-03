using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class RailBlock : AxeInteractable
{
    [SerializeField]
    [Required]
    AxeAbility abilityToInteract = null;
    [SerializeField]
    [Required]
    BoxCollider2D box = null;
    [SerializeField]
    float timeTillDestinationReached = 1f;

    [SerializeField]
    int travelBoxWidth = 2;
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(originPos, blockLeftContraint);
        Gizmos.DrawWireCube(blockLeftContraint, Vector3.one * box.bounds.extents.x * 2);
        Gizmos.DrawLine(originPos, blockRightContraint);
        Gizmos.DrawWireCube(blockRightContraint, Vector3.one * box.bounds.extents.x * 2);
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
        blockLeftContraint = originPos - new Vector3(boxWidth * travelBoxWidth, 0, 0);
        blockRightContraint = originPos + new Vector3(boxWidth * travelBoxWidth, 0, 0);
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
        if (axe.GetAxePosition().x >= this.transform.position.x)
        {
            LogHelper.GetInstance().Log(("Axe hit Block on the Right!").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
            axeHitSide = 1;
        }
        else
        {
            LogHelper.GetInstance().Log(("Axe hit Block on the left!").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
            axeHitSide = -1;
        }
        holderTrans = axe.GetHolder();
    }

    public override void OnAxeAbilityTriggered(AxeAbility triggeredAbility)
    {
        base.OnAxeAbilityTriggered(triggeredAbility);
        if (abilityToInteract.Equals(triggeredAbility))
        {
            if (holderTrans.position.x >= this.transform.position.x && axeHitSide == 1)
            {
                LogHelper.GetInstance().Log(("PULL BLOCK RIGHT!").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
                this.destination = blockRightContraint;
                this.originPos = this.transform.position;
                inMotion = true;
            }
            else if (holderTrans.position.x < this.transform.position.x && axeHitSide == -1)
            {
                LogHelper.GetInstance().Log(("PULL BLOCK LEFT!").Bolden().Colorize(Color.yellow), true, LogHelper.LogLayer.PlayerFriendly);
                this.destination = blockLeftContraint;
                this.originPos = this.transform.position;
                inMotion = true;
            }
        }
    }
}
