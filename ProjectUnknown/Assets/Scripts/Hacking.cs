using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Hacking : MonoBehaviour
{
    [SerializeField]
    PlayerController2D controller2D = null;
    [SerializeField]
    CameraFollow cameraFollow = null;
    [SerializeField]
    Character2D mainCharacter = null;
    [SerializeField]
    Character2D currentCharacter = null;
    [SerializeField]
    float hackDistance = 5f;
    [SerializeField]
    LayerMask hackableLayer;

    private void Start()
    {
        currentCharacter = mainCharacter;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (currentCharacter != null)
        {
            Gizmos.DrawWireSphere(currentCharacter.GetHost().transform.position, hackDistance);
        }
    }
    public void Interact()
    {
        LogHelper.GetInstance().Log(currentCharacter.GetName().Colorize(Color.green).Bolden() + " initiate scanning for interactable", true);
        var cols = Physics2D.OverlapCircleAll(currentCharacter.GetHost().transform.position, hackDistance, hackableLayer);

        foreach (var col in cols)
        {
            var scannedInterable = col.GetComponent<InteractEvent>();
            if (scannedInterable != null)
            {
                scannedInterable.Focus();
                scannedInterable.Interact();
                return;
            }
        }
    }
    public void StartHackingProtocol()
    {
        Character2D targetCharacter = ScanForHackable();
        if (targetCharacter == null)
        {
            LogHelper.GetInstance().Log(currentCharacter.GetName().Colorize(Color.green).Bolden() + " found no possible target", true);
        }
        else
        {
            Hack(targetCharacter);
        }
    }

    private Character2D ScanForHackable()
    {
        LogHelper.GetInstance().Log(currentCharacter.GetName().Colorize(Color.green).Bolden() + " initiate scanning", true);
        var cols = Physics2D.OverlapCircleAll(currentCharacter.GetHost().transform.position, hackDistance, hackableLayer);
        Character2D targetCharacter = null;

        foreach (var col in cols)
        {
            var chip = col.GetComponent<Chip>();
            if (chip != null)
            {
                if (chip.character != currentCharacter)
                {
                    targetCharacter = chip.character;
                    break;
                }
            }
        }


        return targetCharacter;
    }

    private void Hack(Character2D targetCharacter)
    {
        LogHelper.GetInstance().Log(currentCharacter.GetName().Colorize(Color.green).Bolden() + " found target: " + targetCharacter.GetName().Colorize(Color.red).Bolden(), true);
        LogHelper.GetInstance().Log(currentCharacter.GetName().Colorize(Color.green).Bolden() + " initiate " + "hacking protocol".Bolden() + " on " + targetCharacter.GetName().Colorize(Color.red).Bolden(), true);
        LogHelper.GetInstance().Log("Hacking protocol".Bolden() + " on " + targetCharacter.GetName().Colorize(Color.red).Bolden() + "'s result: " + "SUCESS".Bolden().Colorize(Color.green), true);
        controller2D.SetCharacter(targetCharacter);
        currentCharacter.Move(0, 0);
        currentCharacter = targetCharacter;
        cameraFollow.Clear(true);
        cameraFollow.AddEncapsolateObject(currentCharacter.GetHost().transform);
    }
}
