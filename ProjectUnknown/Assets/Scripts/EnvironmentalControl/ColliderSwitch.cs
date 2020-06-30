using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSwitch : MonoBehaviour
{
    [SerializeField]
    Collider2D col = null;
    public void TurnOnCollider()
    {
        col.enabled = true;
    }
    public void TurnOffCollider()
    {
        col.enabled = false;
    }
}
