using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController2D : SingletonMonobehavior<PlayerController2D>
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

    [BoxGroup("Settings")]
    [SerializeField]
    [Required]
    Camera playerCamera = null;
    [BoxGroup("Settings")]
    [SerializeField]
    float jumpInputBufferTime = 0.5f;
    [BoxGroup("Settings")]
    [SerializeField]
    float useAxeInputBuferTime = 0.3f;


    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    float currentJumpBufferTime = 0.0f;
    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    float currentAxeUseBufferTime = 0.0f;
    void Update()
    {
        if (GameMaster.GetInstance().GetStateManager().GetCurrentState().GetEnum().Equals(GameState.GameStateEnum.InGame) == false)
        {
            return;
        }
        var side = 0;
        if (Input.GetKey(KeyCode.W))
        {
            var door = ScanForDoor();
            if (door)
            {
                InteractWithDoor(door);
            }
        }
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
            currentJumpBufferTime = jumpInputBufferTime;
        }
        if (Input.GetMouseButtonDown(0))
        {
            currentAxeUseBufferTime = useAxeInputBuferTime;
        }

        if (currentJumpBufferTime > 0)
        {
            if (Input.GetKey(KeyCode.S) == false)
            {
                if (character.Jump())
                {
                    currentJumpBufferTime = -1;
                }
            }
            else
            {
                if (character.TryDropDown())
                {
                    currentJumpBufferTime = -1;
                }
            }
            currentJumpBufferTime -= Time.deltaTime;
        }

        if (currentAxeUseBufferTime > 0)
        {
            if (grip.IsHoldingAxe())
            {
                if (grip.ThrowAxe(playerCamera.ScreenToWorldPoint(Input.mousePosition)))
                {
                    currentAxeUseBufferTime = -1;
                }
            }
            else
            {
                if (grip.ActivateAxeAbility())
                {
                    currentAxeUseBufferTime = -1;
                }
            }
            currentAxeUseBufferTime -= Time.deltaTime;
        }


    }

    private void InteractWithDoor(LevelTransitionDoor door)
    {
        if (door.IsUsuable())
        {
            TransitionLandingLocation location = door.GetLandingLocation();
            GameMaster.GetInstance().LoadLevelWithLandingIndex(location.targetScene, location.doorIndex);
        }
    }

    private LevelTransitionDoor ScanForDoor()
    {
        var cols = Physics2D.OverlapBoxAll(character.GetHost().transform.position, Vector2.one, 0);
        for (int i = 0; i < cols.Length; i++)
        {
            var doorScript = cols[i].gameObject.GetComponent<LevelTransitionDoor>();
            if (doorScript)
            {
                return doorScript;
            }
        }
        return null;
    }

    public void SetCharacter(Character2D targetCharacter)
    {
        character = targetCharacter;
    }

    public void SetLandingPosition(Vector3 landingPosition)
    {
        character.SetPosition(landingPosition);
    }
}
