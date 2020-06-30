using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AxeDoor : MonoBehaviour
{
    [SerializeField]
    GameMasterSettings settings = null;
    [SerializeField]
    UnityEvent OnOpenAxeDoorEvent = new UnityEvent();
    bool doOnce = true;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag.Equals("Player") && doOnce && settings.isNewGame == false)
        {
            OpenDoor();
            doOnce = false;
        }
    }
    public void OpenDoor()
    {
        OnOpenAxeDoorEvent.Invoke();
    }
}
