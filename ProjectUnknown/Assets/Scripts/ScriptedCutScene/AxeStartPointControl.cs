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
    Vector3 startPos;
    [SerializeField]
    Vector3 startRot;
    [SerializeField]
    Rigidbody2D axeBody;
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
        axeBody.transform.position = startPos;
        axeBehavior.transform.rotation = Quaternion.Euler(startRot);
        grip.SetAxeCatchable(true);
        axe.SetStruck(true);
        grip.ActivateAxeAbility();
    }
}
