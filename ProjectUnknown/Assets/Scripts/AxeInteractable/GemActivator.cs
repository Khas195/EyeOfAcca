using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemActivator : SavePoint
{
    [SerializeField]
    GameMasterSettings settings = null;
    public override void OnAxeHit(Boomeraxe axe)
    {
        if (settings.GemUnlocked == false)
        {
            settings.UnlockGem();
            base.OnAxeHit(axe);
        }
    }
}
