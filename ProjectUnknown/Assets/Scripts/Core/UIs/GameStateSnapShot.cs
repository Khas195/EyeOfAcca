using UnityEngine;

[CreateAssetMenu(fileName = "GameStateSnap", menuName = "Data/GameStateSnap", order = 1)]
public class GameStateSnapShot : ScriptableObject
{
    [SerializeField]
    Camera playerCam = null;
    [SerializeField]
    Vector3 characterPosition = Vector3.one;
    [SerializeField]
    Vector3 axePosition = Vector3.one;
    [SerializeField]
    Color currentGemColor = Color.black;

    public Camera PlayerCamera { get => playerCam; }
    public Vector3 CharacterPosition { get => characterPosition; }
    public Vector3 AxePosition { get => axePosition; }
    public Color GemColor { get => currentGemColor; }
    public void UpdateData(GameStateSnapUpdator updator)
    {
        playerCam = updator.playerCam;
        characterPosition = updator.characterTran.position;
        axePosition = updator.axe.GetAxePosition();
        currentGemColor = updator.axe.GetCurrentAbility().GetGemColor();
    }
}
