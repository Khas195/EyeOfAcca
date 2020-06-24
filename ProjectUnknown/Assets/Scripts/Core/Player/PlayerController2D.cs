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
    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    bool cutJumpVel = false;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        var gameMaster = GameMaster.GetInstance(false);
        if (gameMaster)
        {
            this.SetLandingPosition(gameMaster.GetSpawnLocation());
        }
    }
    public Character2D GetCharacter()
    {
        return this.character;
    }
    void Update()
    {
        var gameMaster = GameMaster.GetInstance();
        if (gameMaster.GetStateManager().GetCurrentState().GetEnum().Equals(GameState.GameStateEnum.InGame) == false)
        {
            return;
        }

        var side = Input.GetAxisRaw("Horizontal");
        var forward = Input.GetAxisRaw("Vertical");
        character.Move(side, forward);

        if (Input.GetKey(KeyCode.W))
        {
            var door = ScanForDoor();
            if (door)
            {
                InteractWithDoor(door);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Input.GetKey(KeyCode.S))
            {
                if (character.TryDropDown() == false)
                {
                    StartJumpBufferTime();
                }
            }
            else
            {
                StartJumpBufferTime();
            }
        }
        if (Input.GetKey(KeyCode.Space) == false && cutJumpVel)
        {
            character.StartFalling();
        }


        if (Input.GetMouseButtonDown(0) && grip.gameObject.activeInHierarchy)
        {
            StartDropdownBufferTime();
        }

        if (currentJumpBufferTime > 0)
        {
            if (character.TryJump())
            {
                currentJumpBufferTime = -1;
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

    private void StartDropdownBufferTime()
    {
        currentAxeUseBufferTime = useAxeInputBuferTime;
    }

    public void StartJumpBufferTime(bool cutJumpVelOnRelease = true)
    {
        currentJumpBufferTime = jumpInputBufferTime;
        this.cutJumpVel = cutJumpVelOnRelease;
    }

    private void InteractWithDoor(LevelTransitionDoor door)
    {
        if (door.IsUsuable())
        {
            TransitionDoorProfile location = door.GetLandingLocation();
            GameMaster.GetInstance().LoadLevelWithLandingDoor(location);
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
