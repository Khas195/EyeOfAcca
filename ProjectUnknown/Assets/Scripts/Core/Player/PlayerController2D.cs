﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController2D : MonoBehaviour
{
    [SerializeField]
    Character2D character = null;
    // Update is called once per frame
    void Update()
    {
        var side = Input.GetAxisRaw("Horizontal");
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
