using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpeningCutScene : MonoBehaviour
{
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
    [SerializeField]
    Transform initialThrowAxePoint = null;
    [SerializeField]
    Transform initialAxePos = null;
    [SerializeField]
    BoomeraxeGrip grip = null;
    // Start is called before the first frame update
    void Start()
    {
        var master = GameMaster.GetInstance(false);
        if (master)
        {
            cutsceneCameraRoot.transform.position = master.GetSpawnLocation();
        }

        if (settings.isNewGame)
        {
            playerCamera.gameObject.SetActive(false);
            cutsceneCameraRoot.gameObject.SetActive(true);
            characterBehaviour.SetActive(false);
            cameraBehaviour.SetActive(false);
            characterAnim.SetBool("StoneTransition", true);
            grip.ThrowAxe(initialThrowAxePoint.position, initialAxePos.transform.position);
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
        settings.isNewGame = false;
        SaveLoadManager.SaveAllData();
    }
}
