using System;
using UnityEngine;

public class Credit : GameState
{
    public override Enum GetEnum()
    {
        return GameStateEnum.Credit;
    }

    public override void OnStateEnter()
    {
        SFXSystem.GetInstance().StopAllSounds();
        if (SFXSystem.GetInstance().IsBGMusicPlaying() == false)
        {
            SFXSystem.GetInstance().PlayBGMusic();
        }
        Time.timeScale = 1.0f;
    }

    public override void OnStateExit()
    {
        SFXSystem.GetInstance().StopAllSounds();
        Time.timeScale = 0.0f;
    }
}
