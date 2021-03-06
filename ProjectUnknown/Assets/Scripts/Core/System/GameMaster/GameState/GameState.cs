using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public abstract class GameState : State
{
    public enum GameStateEnum
    {
        MainMenu,
        InGame,
        GamePaused,
        InInventory,
        Loading,
        Credit
    }
    [SerializeField]
    [ReadOnly]
    protected GameMaster master;

    protected void Awake()
    {
        master = GameMaster.GetInstance();
    }
}
