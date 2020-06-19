using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class OnCollectEvent : UnityEvent<Collectable>
{

}
public class Collectable : MonoBehaviour
{
    public OnCollectEvent OnCollect = new OnCollectEvent();
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            LogHelper.GetInstance().Log("Collect".Colorize(Color.green).Bolden(), true, LogHelper.LogLayer.PlayerFriendly);
            OnCollect.Invoke(this);
        }
    }
}
