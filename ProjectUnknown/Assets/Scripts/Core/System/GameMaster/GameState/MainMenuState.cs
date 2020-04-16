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
        this.master.LoadSceneAdditively("MainMenu");
        return;
    }

    public override void OnStateExit()
    {
        this.master.UnloadScene("MainMenu");
        return;
    }
}
