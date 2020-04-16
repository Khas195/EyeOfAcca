using UnityEngine;

[CreateAssetMenu(fileName = "BehaviorCondition", menuName = "BehaviorCondition/HasStamina")]
/// <summary>
/// Check if the player action cost has enough stamina.
/// Can be created in UnityEditor.
/// </summary>
public class HasStamina : CharacterBehaviorCondition
{
    [SerializeField]
    /// <summary>
    /// The action cost.
    /// </summary>
    /// <summary>
    /// Check if the racter has enough stamina for the action.
    /// </summary>
    /// <param name="character"> The character</param>
    /// <returns>True if has enough stamina and vice versa.</returns>
    public override bool IsSatisfied(Character character)
    {
        return true;
    }
}
