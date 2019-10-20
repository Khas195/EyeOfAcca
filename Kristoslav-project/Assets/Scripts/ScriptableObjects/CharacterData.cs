using UnityEngine;

[CreateAssetMenu(fileName ="CharacterData", menuName ="Data/Character", order = 1)]
public class CharacterData : ScriptableObject {
    public CharacterStatsData statsData;
    public MovementData movementData;
    public Vector3 position;
}
