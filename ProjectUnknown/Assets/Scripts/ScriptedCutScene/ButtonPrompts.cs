using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPrompts : MonoBehaviour
{
    [SerializeField]
    ScaleTransitions scale = null;
    [SerializeField]
    Image prompt = null;
    [SerializeField]
    Transform character = null;


    void LateUpdate()
    {
        if (prompt.enabled == true)
        {
            if (scale.IsCurrentTimeInGraph() == false)
            {
                scale.TransitionIn();
            }

        }
        this.transform.position = character.transform.position;
    }

    public void SetImage(Sprite promptImage)
    {
        prompt.sprite = promptImage;
    }
    public void SetSize(Vector2 size)
    {
        prompt.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
        prompt.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
        prompt.rectTransform.ForceUpdateRectTransforms();

    }
    public void SetImageLocalPosition(Vector2 promptPos)
    {
        prompt.transform.localPosition = promptPos;
    }

    public void TurnOff()
    {
        this.prompt.enabled = false;
    }

    public void TurnOn()
    {
        this.prompt.enabled = true;
    }
}
