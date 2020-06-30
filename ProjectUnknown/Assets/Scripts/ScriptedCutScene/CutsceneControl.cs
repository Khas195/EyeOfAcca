using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneControl : MonoBehaviour
{
    [SerializeField]
    Transform collectAxePosition = null;
    [SerializeField]
    Animator characterAnim = null;
    [SerializeField]
    PlayerController2D playerController = null;
    [SerializeField]
    Character2D character = null;
    [SerializeField]
    GameMasterSettings settings = null;
    bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        if (settings.isNewGame == false)
        {
            this.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            playerController.gameObject.SetActive(false);
            triggered = true;
        }
        if (triggered)
        {
            if (IsInPosition())
            {
                characterAnim.SetBool("CollectAxeCutScene", true);
            }
            else
            {
                MoveToPosition();
            }


        }
    }

    private bool IsInPosition()
    {
        return Mathf.Abs(character.GetHost().transform.position.x - collectAxePosition.position.x) < 2f;
    }

    private void MoveToPosition()
    {
        if (character.GetHost().transform.position.x > collectAxePosition.position.x)
        {
            character.Move(-1, 0);
        }
        else if (character.GetHost().transform.position.x < collectAxePosition.position.x)
        {
            character.Move(1, 0);
        }
    }
}
