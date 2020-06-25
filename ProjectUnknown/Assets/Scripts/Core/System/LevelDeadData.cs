using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDeadData", menuName = "Data/LevelDeadData", order = 1)]
public class LevelDeadData : ScriptableObject, ISaveRestable
{
    [SerializeField]
    public List<Vector2> deadPlaces = new List<Vector2>();
    public void ResetSave()
    {
        deadPlaces.Clear();
    }
}
