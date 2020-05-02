using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController2D : MonoBehaviour
{
    [SerializeField]
    [Required]
    Character2D character = null;
    // Update is called once per frame
    void Update()
    {
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
    }

    public void SetCharacter(Character2D targetCharacter)
    {
        character = targetCharacter;
    }
}
