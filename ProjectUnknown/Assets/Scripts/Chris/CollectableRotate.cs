using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableRotate : MonoBehaviour
{
    [SerializeField]
    private float rotationRate = 0;

    [SerializeField]
    float floatFreq = 0;
    [SerializeField]
    float floatAmp = 0;

    private float timeKeep = 0;

    private Vector3 posInit;
    // Start is called before the first frame update
    void Start()
    {
        this.posInit = this.transform.localPosition;
        this.timeKeep = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.RotateAround(this.transform.position, -1 * Vector3.up, rotationRate * Time.deltaTime);

        this.transform.localPosition = posInit + Vector3.up * floatAmp * Mathf.Sin(floatFreq * timeKeep);

        timeKeep += Time.deltaTime;
    }
}
