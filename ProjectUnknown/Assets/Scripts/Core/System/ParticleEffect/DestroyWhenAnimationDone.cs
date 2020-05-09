using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class DestroyWhenAnimationDone : MonoBehaviour
{
    public void DestroySelf()
    {
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
