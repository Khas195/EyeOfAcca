using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailActivator : SavePoint
{
    [SerializeField]
    GameMasterSettings settings = null;
    public override void OnAxeHit(Boomeraxe axe)
    {
        if (settings.RailUnlocked == false)
        {
            settings.UnlockRail();
            base.OnAxeHit(axe);
        }
    }
}
