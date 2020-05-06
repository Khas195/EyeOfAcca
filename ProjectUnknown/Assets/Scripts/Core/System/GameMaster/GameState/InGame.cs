using System;

public class InGame : GameState
{
    public override Enum GetEnum()
    {
        return GameStateEnum.InGame;
    }

    public override void OnStateEnter()
    {
        if (SFXSystem.GetInstance().IsBGMusicPlaying() == false)
        {
            SFXSystem.GetInstance().PlayBGMusic();
        }
    }

    public override void OnStateExit()
    {
    }
}
