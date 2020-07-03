using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScene : MonoBehaviour
{
    [SerializeField]
    TransitionCurve continueFade = null;
    [SerializeField]
    TransitionCurve thankyouFade = null;
    [SerializeField]
    TransitionCurve thankyouMove = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            continueFade.TransitionIn(ThankyouFade);
        }
    }

    public void ThankyouFade()
    {
        thankyouFade.TransitionIn(ThankyouMove);
    }
    public void ThankyouMove()
    {
        thankyouMove.TransitionIn();
    }
}
