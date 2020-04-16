using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public abstract class IInteractable : MonoBehaviour
{
    [SerializeField]
    [ReadOnly]
    protected bool isFocus;

    public virtual void Focus()
    {
        LogHelper.GetInstance().Log("Focusing".Bolden() + " " + this.name.Colorize("cyan").Bolden(), true);
        isFocus = true;
    }
    public virtual void Defocus()
    {
        LogHelper.GetInstance().Log("Defocusing".Bolden() + " " + this.name.Colorize("cyan").Bolden(), true);
        isFocus = false;
    }

    public virtual bool Interact()
    {

        LogHelper.GetInstance().Log(this.GetKindOfInteraction().Bolden() + " " + this.name.Colorize("cyan").Bolden(), true);
        return isFocus;
    }

    public bool IsFocus()
    {
        return isFocus;
    }

    public virtual string GetKindOfInteraction()
    {
        return "";
    }
}
