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
        fadeTrans.TransitionOut();
        scaleTrans.TransitionOut();
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
    void Update()
    {
        var axe = BoomeraxeGrip.GetInstance(false);
        var playerCam = CameraFollow.GetInstance(false);
        if (axe == null || playerCam == null) return;
        Vector3 pos = axe.GetAxePosition();
        pos.z = playerCam.GetCamera().nearClipPlane;
        rotateScript.SetPivot(axeIndicatorPivot);
        var axeScreenPoint = playerCam.GetCamera().WorldToScreenPoint(pos);

        if (axeScreenPoint.x <= 0 || axeScreenPoint.x >= Screen.width || axeScreenPoint.y <= 0 || axeScreenPoint.y >= Screen.height)
        {
            if (showIndicator == false)
            {
                showIndicator = true;
                fadeTrans.TransitionIn();
                scaleTrans.TransitionIn();
            }
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

        if (axeScreenPoint.x <= 0)
        {
            axeScreenPoint.x = 0 + axeIndicator.sprite.rect.width * 4;
        }
        if (axeScreenPoint.x >= Screen.width)
        {
            axeScreenPoint.x = Screen.width - axeIndicator.sprite.rect.width * 4;
        }

        if (axeScreenPoint.y <= 0)
        {
            axeScreenPoint.y = 0 + axeIndicator.sprite.rect.height * 4;
        }
        if (axeScreenPoint.y >= Screen.height)
        {
            axeScreenPoint.y = Screen.height - axeIndicator.sprite.rect.height * 4;
        }
        if (showIndicator == true)
        {
            rotateScript.RotateXAxisToward(playerCam.GetCamera().WorldToScreenPoint(pos));
            targetPos = axeScreenPoint;
            axeIndicatorPivot.transform.position = Vector3.Lerp(axeIndicatorPivot.transform.position, targetPos, Time.deltaTime * moveSpeed);
        }


        axeIndicator.color = axe.GetAxeFlying().GetCurrentAbility().GetGemColor();

        var indicatorColor = axeIndicator.color;
        axe.GetAxeFlying();
        indicatorColor.a = fadeTrans.GetCurrentValue();
        axeIndicator.color = indicatorColor;
        fadeTrans.AdvanceTime(Time.deltaTime);

        var scale = axeIndicator.transform.localScale;
        scale.x = scale.y = scaleTrans.GetCurrentValue();
        axeIndicator.transform.localScale = scale;
        scaleTrans.AdvanceTime(Time.deltaTime);
    }
}
