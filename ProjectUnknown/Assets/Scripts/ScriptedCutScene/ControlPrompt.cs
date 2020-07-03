using UnityEngine;

[CreateAssetMenu(fileName = "ControlPrompt", menuName = "Data/ControlPrompt", order = 1)]
public class ControlPrompt : ScriptableObject
{
    public Sprite promptImage = null;
    public bool showAboveCharacter = true;
}
