using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractEvent : IInteractable
{
    [SerializeField]
    string interactionName = "";
    [SerializeField]
    UnityEvent onFocus = new UnityEvent();
    [SerializeField]
    UnityEvent onDeFocus = new UnityEvent();

    [SerializeField]
    UnityEvent onInteract = new UnityEvent();

    public override void Defocus()
    {
        base.Defocus();
        onDeFocus.Invoke();
    }

    public override void Focus()
    {
        base.Focus();
        onFocus.Invoke();
    }

    public override string GetKindOfInteraction()
    {
        return interactionName;
    }

    public override bool Interact()
    {
        if (base.Interact())
        {
            onInteract.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }
}
