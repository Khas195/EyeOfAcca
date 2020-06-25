using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
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
    Transform initialThrowAxePoint = null;
    [SerializeField]
    Transform initialAxePos = null;
    [SerializeField]
    BoomeraxeGrip grip = null;
    [SerializeField]
    GameObject axeEntity = null;
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
                grip.ThrowAxe(initialThrowAxePoint.position, initialAxePos.position);
            }
        }
        else
        {
            grip.HoldAxe(force: true);
        }

    }
}
