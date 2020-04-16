using UnityEngine;

[CreateAssetMenu(fileName = "IsInPausedMenu", menuName = "BehaviorCondition/IsInPausedMenu")]
public class IsInPausedMenu : CharacterBehaviorCondition
{
    [SerializeField]
    bool reverse = false;
    public override bool IsSatisfied(Character character)
    {
        var isInState = GameMaster.GetInstance().IsInState(GameState.GameStateEnum.GamePaused);
        return reverse ? !isInState : isInState;
    }
}