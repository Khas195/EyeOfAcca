using UnityEngine;

[CreateAssetMenu(fileName = "GameStateSnap", menuName = "Data/GameStateSnap", order = 1)]
public class GameStateSnapShot : ScriptableObject
{
    [SerializeField]
    Camera playerCam = null;
    [SerializeField]
    Vector3 characterPosition = Vector3.one;
    [SerializeField]
    Vector3 characterVelocity = Vector3.one;
    [SerializeField]
    Vector3 axePosition = Vector3.one;
    [SerializeField]
    Color currentGemColor = Color.black;
    [SerializeField]
    private bool isHoldingAxe;

    public Camera PlayerCamera { get => playerCam; }
    public Vector3 CharacterPosition { get => characterPosition; }
    public Vector3 AxePosition { get => axePosition; }
    public Color GemColor { get => currentGemColor; }
    public bool IsHoldingAxe { get => isHoldingAxe; }
    public Vector3 CharacterVelocity { get => characterVelocity; }

    public void UpdateData(GameStateSnapUpdator updator)
    {
        playerCam = updator.playerCam;
        characterPosition = updator.characterTran.position;
        axePosition = updator.axe.GetAxePosition();
        currentGemColor = updator.axe.GetCurrentAbility().GetGemColor();
        this.isHoldingAxe = updator.grip.IsHoldingAxe();
        this.characterVelocity = updator.characterBody.velocity;
    }
}
