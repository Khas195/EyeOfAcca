using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : SingletonMonobehavior<InGameMenu>
{
    [SerializeField]
    [Required]
    Animator animator = null;
    [SerializeField]
    GameObject axeIndicatorPivot = null;
    [SerializeField]
    Image axeIndicator;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    RotateToward rotateScript = null;

    [SerializeField]
    TransitionCurve fadeTrans = null;
    [SerializeField]
    TransitionCurve scaleTrans = null;

    bool showIndicator = false;
    Vector3 targetPos;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
    }
    public void HideInGameMenu()
    {
        animator.SetTrigger("HideInGameMenu");
    }
    public void ShowInGameMenu()
    {
        animator.SetTrigger("ShowInGameMenu");
    }
    public void ResumeGame()
    {
        GameMaster.GetInstance().ResumeGame();
    }
    public void ExitGame()
    {
        GameMaster.GetInstance().ExitGame();
    }
    public void GoToMainMenu()
    {
        GameMaster.GetInstance().GoToMainMenu();
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void LateUpdate()
    {
        var axe = BoomeraxeGrip.GetInstance(false);
        var playerCam = CameraFollow.GetInstance(false);
        var playerControl = PlayerController2D.GetInstance(false);
        rotateScript.SetPivot(axeIndicatorPivot);
        if (axe == null || playerCam == null || playerControl == null) return;

        Vector3 axePos = axe.GetAxePosition();
        var axeScreenPos = playerCam.GetCamera().WorldToScreenPoint(axePos);

        if (IsPosOffCameraView(axeScreenPos))
        {
            if (showIndicator == false)
            {
                showIndicator = true;
                fadeTrans.TransitionIn();
                scaleTrans.TransitionIn();
            }
            UpdateIndicatorPosition(playerCam, playerControl, axePos);

        }
        else
        {
            if (showIndicator == true)
            {
                scaleTrans.TransitionOut();
                fadeTrans.TransitionOut();
                showIndicator = false;
            }
        }
        ProcessTransition(axe);
    }

    private static bool IsPosOffCameraView(Vector3 posToCheck)
    {
        return posToCheck.x <= 0 || posToCheck.x >= Screen.width || posToCheck.y <= 0 || posToCheck.y >= Screen.height;
    }

    private void UpdateIndicatorPosition(CameraFollow playerCam, PlayerController2D playerControl, Vector3 axePos)
    {
        var characterTrans = playerControl.GetCharacter().GetHost();

        Vector3 borderPos = GetIntersectPositionBetweenABAndCamera(axePos, characterTrans.transform.position);

        var indicatorPos = playerCam.GetCamera().WorldToScreenPoint(borderPos);

        indicatorPos.x = Mathf.Clamp(indicatorPos.x, axeIndicator.rectTransform.sizeDelta.x * 2f, Screen.width - axeIndicator.rectTransform.sizeDelta.x * 2f);
        indicatorPos.y = Mathf.Clamp(indicatorPos.y, axeIndicator.rectTransform.sizeDelta.y * 2f, Screen.height - axeIndicator.rectTransform.sizeDelta.y * 2f);

        rotateScript.RotateXAxisToward(playerCam.GetCamera().WorldToScreenPoint(axePos));

        targetPos = indicatorPos;
        axeIndicatorPivot.transform.position = Vector3.Lerp(axeIndicatorPivot.transform.position, targetPos, Time.deltaTime * moveSpeed);
    }

    private static Vector3 GetIntersectPositionBetweenABAndCamera(Vector3 posA, Vector3 posB)
    {
        var hits = Physics2D.RaycastAll(posA, (posB - posA).normalized);

        Vector3 borderPos = Vector3.zero;
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.tag.Equals("MainCamera"))
            {
                borderPos = hits[i].point;
                break;
            }
        }

        return borderPos;
    }

    private void ProcessTransition(BoomeraxeGrip axe)
    {
        axeIndicator.color = axe.GetAxeFlying().GetCurrentAbility().GetGemColor();
        var indicatorColor = axeIndicator.color;
        indicatorColor.a = fadeTrans.GetCurrentValue();
        axeIndicator.color = indicatorColor;
        fadeTrans.AdvanceTime(Time.deltaTime);

        var scale = axeIndicator.transform.localScale;
        scale.x = scale.y = scaleTrans.GetCurrentValue();
        axeIndicator.transform.localScale = scale;
        scaleTrans.AdvanceTime(Time.deltaTime);
    }
}
