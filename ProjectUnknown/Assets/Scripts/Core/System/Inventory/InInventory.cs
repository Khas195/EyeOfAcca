using System;
using UnityEngine;

public class InInventory : GameState
{
    public override Enum GetEnum()
    {
        return GameStateEnum.InInventory;
    }

    public override void OnStateEnter()
    {
        InventorySystem.GetInstance().ShowUI();
        GameMaster.GetInstance().SetMouseVisibility(true);
        this.gameObject.SetActive(true);
    }

    public override void OnStateExit()
    {
        InventorySystem.GetInstance().HideUI();
        GameMaster.GetInstance().SetMouseVisibility(false);
        this.gameObject.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Escape))
        {
            GameMaster.GetInstance().ResumeGame();
        }
    }
}
