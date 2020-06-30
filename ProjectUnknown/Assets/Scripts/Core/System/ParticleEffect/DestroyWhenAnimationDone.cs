using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class DestroyWhenAnimationDone : MonoBehaviour
{
    public UnityEvent OnAnimationDone = new UnityEvent();
    public void DestroySelf()
    {
        OnAnimationDone.Invoke();
        Destroy(this.gameObject);
    }
    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        DestroySelf();
    }
}
