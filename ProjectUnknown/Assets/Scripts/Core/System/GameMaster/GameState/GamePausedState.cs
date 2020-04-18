using System;

public class GamePausedState : GameState
{
    public override Enum GetEnum()
    {
        return GameStateEnum.GamePaused;
    }

    public override void OnStateEnter()
    {
        GameMaster.GetInstance().SetGameTimeScale(0.0f);
        InGameMenu.GetInstance().ShowInGameMenu();
    }

    public override void OnStateExit()
    {
        GameMaster.GetInstance().SetGameTimeScale(1.0f);
        InGameMenu.GetInstance().HideInGameMenu();
    }
}
