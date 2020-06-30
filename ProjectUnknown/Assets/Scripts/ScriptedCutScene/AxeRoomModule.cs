using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AxeRoomModule : MonoBehaviour
{
    [SerializeField]
    GameMasterSettings settings = null;
    [SerializeField]
    Animator playerAnim = null;
    [SerializeField]
    GameObject playerControl = null;
    [SerializeField]
    Character2D character = null;
    bool playerInRange = false;
    [SerializeField]
    Shake fakeAxeShake = null;
    [SerializeField]
    FakeAxe fakeAxe = null;
    [SerializeField]
    AxeStartPointControl control = null;
    [SerializeField]
    [Scene]
    string PickUpAxeScene = "";


    void Start()
    {
        if (settings.isNewGame == false || SceneManager.GetSceneByName(PickUpAxeScene).IsValid() == false)
        {
            this.gameObject.SetActive(false);
            return;
        }
        PlayerController2D.OnPlayerRequestUseAxe.AddListener(this.OnPlayerTryUseAxe);
        AxeRoomCharacterAnimationEvent.OnRaisingArmsDone.AddListener(TryPullAxe);
        AxeRoomCharacterAnimationEvent.OnRaisingArmsDone.AddListener(GivePlayerControl);
    }
    public void GivePlayerControl()
    {
        playerControl.gameObject.SetActive(true);
    }
    public void OnPlayerTryUseAxe()
    {
        if (playerInRange)
        {
            playerControl.gameObject.SetActive(false);
            character.Move(0, 0);
            playerAnim.SetBool("CollectAxeCutscene", true);
        }
    }
    public void TryPullAxe()
    {
        fakeAxeShake.InduceTrauma(PlayPullAxeAnimation);
        fakeAxe.GatherPower();
    }
    public void PlayPullAxeAnimation()
    {
        playerAnim.SetTrigger("AxeCutSceneNext");
        playerAnim.SetBool("CollectAxeCutscene", false);
        this.control.ForceRecall();
        this.gameObject.SetActive(false);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            playerInRange = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            playerInRange = true;
        }
    }
}
