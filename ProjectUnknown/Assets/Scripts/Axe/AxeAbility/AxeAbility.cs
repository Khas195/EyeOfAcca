using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AxeAbility : ScriptableObject
{
    public abstract void Activate(Boomeraxe axe);

    public abstract string GetAbilityPower();
}
