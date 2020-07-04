using UnityEngine;

public class PromptArea : MonoBehaviour
{
    [SerializeField]
    ControlPrompt promptToShow = null;
    public ControlPrompt GetPrompt()
    {
        return promptToShow;
    }
}
