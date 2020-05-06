using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController2D : MonoBehaviour
{
    [BoxGroup("Settings")]
    [SerializeField]
    [Required]
    Character2D character = null;

    [BoxGroup("Settings")]
    [SerializeField]
    [Required]
    BoomeraxeGrip grip = null;
    // Update is called once per frame
    void Update()
    {
        if (GameMaster.GetInstance().GetStateManager().GetCurrentState().GetEnum().Equals(GameState.GameStateEnum.InGame) == false)
        {
            return;
        }
        var side = 0;
        if (Input.GetKey(KeyCode.A))
        {
            side = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            side = 1;
        }
        var forward = Input.GetAxisRaw("Vertical");
        character.Move(side, forward);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            character.Jump();

        }
        if (Input.GetMouseButtonDown(0))
        {
            grip.UseAxe();
        }
    }

    public void SetCharacter(Character2D targetCharacter)
    {
        character = targetCharacter;
    }
}
