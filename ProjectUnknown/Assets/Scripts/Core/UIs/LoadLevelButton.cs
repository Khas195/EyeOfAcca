using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LoadLevelButton : MonoBehaviour
{
    [SerializeField]
    bool loadDesignatedLevel = false;
    [SerializeField]
    [ShowIf(" loadDesignatedLevel")]
    TransitionDoorProfile levelDoor = null;
    [SerializeField]
    Button button = null;

    void Awake()
    {
        FindButton();
    }
    [Button("Assign Button")]
    public void FindButton()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(LoadLevel);
    }
    public void LoadLevel()
    {
        var master = GameMaster.FindInstance();
        if (loadDesignatedLevel)
        {
            LogHelper.GetInstance().Log(this + " loading level with level index " + levelDoor);
        }
        master.InitiateLoadLevelSequence(levelDoor);
    }
}
