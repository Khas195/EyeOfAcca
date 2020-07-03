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
    [SerializeField]
    TransitionCurve creditsMove = null;
    [SerializeField]
    TransitionCurve buttonsFade = null;

    void Start()
    {
        continueFade.TransitionIn(ThankyouFade);
    }


    public void ThankyouFade()
    {
        thankyouFade.TransitionIn(ThankyouMove);
    }
    public void ThankyouMove()
    {
        thankyouMove.TransitionIn(CreditMove);
    }
    public void CreditMove()
    {
        creditsMove.TransitionIn(ButtonFade);
    }

    public void ButtonFade()
    {
        buttonsFade.TransitionIn();
    }

    public void NewGame()
    {
        GameMaster.GetInstance().StartNewGame();
    }
    public void MainMenu()
    {
        GameMaster.GetInstance().GoToMainMenu();
    }
}
