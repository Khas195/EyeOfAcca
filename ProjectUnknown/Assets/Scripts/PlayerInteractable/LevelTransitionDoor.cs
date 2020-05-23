using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class LevelTransitionDoor : MonoBehaviour
{
    [SerializeField]
    [Required]
    TransitionLandingLocation landingPlace = null;

    [SerializeField]
    bool doorUsuable = true;

    public bool IsUsuable()
    {
        return doorUsuable;
    }

    public TransitionLandingLocation GetLandingLocation()
    {
        return landingPlace;
    }
}
