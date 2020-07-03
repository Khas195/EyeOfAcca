using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PromptsMaster : SingletonMonobehavior<PromptsMaster>
{
    [SerializeField]
    ButtonPrompts prompt = null;
    [SerializeField]
    Vector2 abovePos = Vector2.one;
    [SerializeField]
    Vector2 belowPos = Vector2.one;
    [SerializeField]
    Vector2 scanSize = Vector2.one;
    [SerializeField]
    LayerMask promptAreaMask = 0;
    [Scene]
    public List<string> tutorialScenes = new List<string>();
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(this.prompt.transform.position, scanSize);
    }
    void Start()
    {
        bool turnOff = true;
        for (int i = 0; i < tutorialScenes.Count; i++)
        {
            if (SceneManager.GetSceneByName(tutorialScenes[i]).IsValid())
            {
                turnOff = false;
                break;
            }
        }
        if (turnOff)
        {
            this.gameObject.SetActive(false);
        }
    }
    public void ScanForPromptArea()
    {
        var col = Physics2D.OverlapBox(this.prompt.transform.position, scanSize, 0, promptAreaMask);
        if (col)
        {
            var promptArea = col.GetComponent<PromptArea>();
            this.ShowPrompt(promptArea.GetPrompt());
        }
        else
        {
            HidePrompt();
        }
    }
    void FixedUpdate()
    {
        ScanForPromptArea();
    }
    public void ShowPrompt(ControlPrompt controlPrompt)
    {
        if (controlPrompt.showAboveCharacter)
        {
            prompt.SetImageLocalPosition(abovePos);
        }
        else
        {
            prompt.SetImageLocalPosition(belowPos);
        }
        prompt.SetImage(controlPrompt.promptImage);
        prompt.SetSize(controlPrompt.size);
        prompt.TurnOn();
    }
    public void HidePrompt()
    {
        prompt.TurnOff();
    }

}
