using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InGameMap : MonoBehaviour
{
    [SerializeField]
    [Required]
    UnityEvent showMap = new UnityEvent();

    [SerializeField]
    [Required]
    UnityEvent hideMap = new UnityEvent();
    [SerializeField]
    [ReadOnly]
    bool isMapShown = false;

    void Update()
    {
        if (GameMaster.GetInstance().IsInState(GameState.GameStateEnum.InGame))
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (isMapShown)
                {
                    hideMap.Invoke();
                }
                else
                {
                    showMap.Invoke();
                }
                isMapShown = !isMapShown;
            }
        }
        else
        {
            if (isMapShown)
            {
                hideMap.Invoke();
                isMapShown = false;
            }
        }
    }
}
