using System;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDoor", menuName = "Data/LevelDoor", order = 1)]
[Serializable]
public class TransitionDoorProfile : ScriptableObject
{
    [Scene]
    [SerializeField]
    public string doorHome = "";

    [ReadOnly]
    [SerializeField]
    public Vector3 doorLocation = Vector3.one;

    public TransitionDoorProfile landingPlace = null;
    public void SetLandPlace(TransitionDoorProfile newLandPlace)
    {
        this.landingPlace = newLandPlace;
    }

}
