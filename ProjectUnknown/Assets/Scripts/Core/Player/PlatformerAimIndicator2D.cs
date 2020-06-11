using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PlatformerAimIndicator2D : MonoBehaviour
{
    [SerializeField]
    [Required]
    GameObject pivot = null;
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    RotateToward rotateScript = null;
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    Transform characterTrans = null;
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    SpriteRenderer indicatorSprite = null;



    [BoxGroup("Settings")]
    [SerializeField]
    TransitionCurve fade = null;
    [BoxGroup("Settings")]
    [SerializeField]
    TransitionCurve scaleTrans = null;

    Vector2 aimOriginPos;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        aimOriginPos = indicatorSprite.transform.localPosition;
        Show();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMaster.GetInstance().IsInState(GameState.GameStateEnum.InGame) == false)
        {
            return;
        }
        pivot.transform.position = characterTrans.transform.position;
        ProcessTransition();
    }

    private void ProcessTransition()
    {
        var indicatorColor = indicatorSprite.color;
        indicatorColor.a = fade.GetCurrentValue();
        indicatorSprite.color = indicatorColor;
        fade.AdvanceTime(Time.deltaTime);

        var scale = indicatorSprite.transform.localScale;
        scale.y = scale.x = scaleTrans.GetCurrentValue();
        indicatorSprite.transform.localPosition = aimOriginPos + scale.x * Vector2.right;
        indicatorSprite.transform.localScale = scale;
        scaleTrans.AdvanceTime(Time.deltaTime);
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        rotateScript.RotateXAxisTowardMouse();
    }
    public void Show()
    {
        fade.TransitionIn();
        scaleTrans.TransitionIn();
    }
    public void Hide()
    {
        fade.TransitionOut();
        scaleTrans.TransitionOut();
    }
}
