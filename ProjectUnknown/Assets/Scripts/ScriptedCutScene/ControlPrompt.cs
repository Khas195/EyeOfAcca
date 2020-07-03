using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "ControlPrompt", menuName = "Data/ControlPrompt", order = 1)]
public class ControlPrompt : ScriptableObject
{
    [ShowAssetPreview]
    public Sprite promptImage = null;
    public bool showAboveCharacter = true;
    public Vector2 size = Vector2.one;
}
