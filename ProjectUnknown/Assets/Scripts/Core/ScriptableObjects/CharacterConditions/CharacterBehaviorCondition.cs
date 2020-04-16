using UnityEngine;

/// <summary>
/// Check a certain condtion to see if it satisfied .
/// </summary>
public class CharacterBehaviorCondition : ScriptableObject
{
    public virtual bool IsSatisfied(Character character)
    {
        return true;
    }
}
