using System;

public class LoadingState : GameState
{
    public override Enum GetEnum()
    {
        return GameStateEnum.Loading;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }
}
