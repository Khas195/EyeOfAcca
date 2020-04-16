using UnityEngine;

[CreateAssetMenu(fileName = "BehaviorCondition", menuName = "BehaviorCondition/TouchingGround")]
/// <summary>
/// Check if the character is touching ground.
/// Can be create in Unity Editor.
/// </summary>
public class IsTouchingGround : CharacterBehaviorCondition
{
    /// <summary>
    /// Check if the character is touching ground.
    /// </summary>
    /// <param name="character">The character</param>
    /// <returns>true: if the character is touching ground and vice versa.</returns>
    public override bool IsSatisfied(Character character)
    {
        return character.IsTouchingGround();
    }
}
