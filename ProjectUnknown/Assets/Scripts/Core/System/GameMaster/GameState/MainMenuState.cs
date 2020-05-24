using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainMenuState : GameState
{
    public override Enum GetEnum()
    {
        return GameStateEnum.MainMenu;
    }

    public override void OnStateEnter()
    {
        SFXSystem.GetInstance().StopAllSounds();
        SFXSystem.GetInstance().PlayBGMusic();
    }

    public override void OnStateExit()
    {
    }
}
