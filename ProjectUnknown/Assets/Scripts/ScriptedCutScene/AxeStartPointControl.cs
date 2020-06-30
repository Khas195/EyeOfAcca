using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class AxeStartPointControl : MonoBehaviour
{
    [SerializeField]
    [Scene]
    string OpeningSceneNoAxe = "";
    [SerializeField]
    [Scene]
    string PickUpAxeScene = "";
    [SerializeField]
    GameMasterSettings settings = null;

    [SerializeField]
    BoomeraxeGrip grip = null;
    [SerializeField]
    Boomeraxe axe = null;
    [SerializeField]
    GameObject axeEntity = null;
    [SerializeField]
    public GameObject axeBehavior = null;
    [SerializeField]
    Transform startPos = null;
    [SerializeField]
    Rigidbody2D axeBody = null;
    void Awake()
    {
        if (settings.isNewGame)
        {
            if (SceneManager.GetSceneByName(OpeningSceneNoAxe).IsValid())
            {
                axeEntity.SetActive(false);
            }
            else if (SceneManager.GetSceneByName(PickUpAxeScene).IsValid())
            {
                axeBehavior.SetActive(false);
            }
        }
        else
        {
            grip.HoldAxe(force: true);
        }
    }
    public void ForceRecall()
    {
        axeBehavior.SetActive(true);
        axeBody.gameObject.SetActive(true);
        axeBody.transform.position = startPos.transform.position;
        grip.SetAxeCatchable(true);
        axe.SetStruck(true);
        grip.ActivateAxeAbility();
    }
}
