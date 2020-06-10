using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
public class InGameMenu : SingletonMonobehavior<InGameMenu>
{
    [SerializeField]
    [Required]
    GameStateSnapShot snapShot;
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
        rotateScript.SetPivot(axeIndicatorPivot);
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
    void LateUpdate()
    {
        if (snapShot.PlayerCamera == null) return;

        Vector3 axePos = snapShot.AxePosition;
        var axeScreenPos = snapShot.PlayerCamera.WorldToScreenPoint(axePos);

        if (IsPosOffCameraView(axeScreenPos))
        {
            if (showIndicator == false)
            {
                showIndicator = true;
                fadeTrans.TransitionIn();
                scaleTrans.TransitionIn();
            }
            UpdateIndicatorPosition(snapShot.PlayerCamera, snapShot.CharacterPosition, snapShot.AxePosition);

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
        ProcessTransition(snapShot.GemColor);
    }

    private void ProcessTransition(object gemColor)
    {
        throw new NotImplementedException();
    }

    private static bool IsPosOffCameraView(Vector3 posToCheck)
    {
        return posToCheck.x <= 0 || posToCheck.x >= Screen.width || posToCheck.y <= 0 || posToCheck.y >= Screen.height;
    }

    private void UpdateIndicatorPosition(Camera playerCam, Vector3 characterPos, Vector3 axePos)
    {

        Vector3 borderPos = GetIntersectPositionBetweenABAndCamera(axePos, characterPos);

        var indicatorPos = playerCam.WorldToScreenPoint(borderPos);

        indicatorPos.x = Mathf.Clamp(indicatorPos.x, axeIndicator.rectTransform.sizeDelta.x * 2f, Screen.width - axeIndicator.rectTransform.sizeDelta.x * 2f);
        indicatorPos.y = Mathf.Clamp(indicatorPos.y, axeIndicator.rectTransform.sizeDelta.y * 2f, Screen.height - axeIndicator.rectTransform.sizeDelta.y * 2f);

        rotateScript.RotateXAxisToward(playerCam.WorldToScreenPoint(axePos));

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

    private void ProcessTransition(Color gemColor)
    {
        axeIndicator.color = gemColor;
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
