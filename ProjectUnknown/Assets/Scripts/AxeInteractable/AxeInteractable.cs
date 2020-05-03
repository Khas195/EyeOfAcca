using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AxeInteractable : MonoBehaviour
{
    public virtual void OnAxeHit(Boomeraxe axe)
    {
        return;
    }
    // This function need to be linked manually via Axe SetActiveAbility in OnAxeHit.
    public virtual void OnAxeAbilityTriggered(AxeAbility triggeredAbility)
    {
        return;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        ProcessCollider(other);
    }

    private void ProcessCollider(Collider2D other)
    {
        if (other.tag.Equals("Boomeraxe"))
        {
            var axeColisionDetector = other.GetComponent<BoomeraxeCollisionDetection>();
            if (axeColisionDetector == null)
            {
                LogHelper.GetInstance().LogError("Axe had collided with an Axe interactable without an axe collision detector script attached", true);
                return;
            }
            var axe = axeColisionDetector.GetAxe();
            if (axe == null)
            {
                LogHelper.GetInstance().LogError("Axe had collided with an Axe interactable without an axe reference in axe collision detector script ", true);
                return;
            }
            if (axe.IsInThrowMotion())
            {
                OnAxeHit(axe);
                axe.AddActiveAbilityCallback(OnAxeAbilityTriggered);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        ProcessCollider(collisionInfo.collider);
    }

}
