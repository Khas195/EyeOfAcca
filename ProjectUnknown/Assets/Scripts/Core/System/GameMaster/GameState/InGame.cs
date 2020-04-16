using System;

public class InGame : GameState
{
    public override Enum GetEnum()
    {
        return GameStateEnum.InGame;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }
}
