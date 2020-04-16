using UnityEngine;

[CreateAssetMenu(fileName = "IsAttacking", menuName = "BehaviorCondition/IsAttacking")]
public class IsAttacking : CharacterBehaviorCondition
{
    public override bool IsSatisfied(Character character)
    {
        return false;
    }
}
