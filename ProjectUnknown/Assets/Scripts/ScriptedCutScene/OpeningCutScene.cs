using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class OpeningCutScene : MonoBehaviour
{
    [SerializeField]
    [Scene]
    string openingSceneName = "";
    [SerializeField]
    GameMasterSettings settings = null;
    [SerializeField]
    Camera playerCamera = null;
    [SerializeField]
    Animator cutsceneAnim = null;
    [SerializeField]
    Animator characterAnim = null;
    [SerializeField]
    PlayerController2D characterControl = null;
    [SerializeField]
    GameObject cutsceneCameraRoot = null;
    [SerializeField]
    GameObject characterBehaviour = null;
    [SerializeField]
    GameObject cameraBehaviour = null;
    [SerializeField]
    Shake cameraShake = null;
    [SerializeField]
    Shake characterShake = null;
    [SerializeField]
    List<Transform> stoneBreaksPos = new List<Transform>();
    int curBreakCount = 0;

    void Start()
    {
        var master = GameMaster.GetInstance(false);
        if (master)
        {
            cutsceneCameraRoot.transform.position = master.GetSpawnLocation();
        }
        if (settings.isNewGame && SceneManager.GetSceneByName(openingSceneName).IsValid())
        {
            playerCamera.gameObject.SetActive(false);
            cutsceneCameraRoot.gameObject.SetActive(true);
            characterBehaviour.SetActive(false);
            cameraBehaviour.SetActive(false);
            characterAnim.SetBool("StoneTransition", true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cutsceneAnim.SetTrigger("Next");
        }
    }
    public void TriggerNext()
    {
        characterAnim.SetTrigger("NextTransition");
        cameraShake.InduceTrauma();
        characterShake.InduceTrauma();
        if (stoneBreaksPos.Count > curBreakCount)
        {
            VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.CharacterStoneBreaks, stoneBreaksPos[curBreakCount].position, Quaternion.identity);
            curBreakCount += 1;

        }
    }
    public void TriggerLast()
    {
        TriggerNext();
        characterAnim.SetBool("StoneTransition", false);
        characterControl.StartJumpBufferTime(false);
        characterBehaviour.SetActive(true);
    }
    public void Finished()
    {
        playerCamera.gameObject.SetActive(true);
        cutsceneCameraRoot.gameObject.SetActive(false);
        cameraBehaviour.SetActive(true);
        SaveLoadManager.SaveAllData();
    }
}
