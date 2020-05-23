using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDoor", menuName = "Data/LevelDoor", order = 1)]
public class TransitionLandingLocation : ScriptableObject
{
    [Scene]
    public string targetScene = "";
    public int doorIndex = 0;
}
