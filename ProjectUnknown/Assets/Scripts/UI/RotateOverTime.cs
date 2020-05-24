using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RotateOverTime : MonoBehaviour
{
    [SerializeField]
    float rotateSpeed = 20f;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(-1 * Vector3.forward * (rotateSpeed * Time.unscaledDeltaTime));
    }
}
