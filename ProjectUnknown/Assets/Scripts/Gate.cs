using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public void Open()
    {
        this.gameObject.SetActive(true);
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }
    public void Switch()
    {
        if (this.gameObject.activeSelf)
        {
            Close();
        }
        else
        {
            Open();
        }
    }
}
