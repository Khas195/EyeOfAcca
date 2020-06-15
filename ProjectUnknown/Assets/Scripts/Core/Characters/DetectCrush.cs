using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class DetectCrush : MonoBehaviour
{
    [SerializeField]
    [Required]
    Chip chip = null;
    [SerializeField]
    LayerMask layerToDetect = 0;
    [SerializeField]
    Vector3 checkSize = Vector3.one;
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position, checkSize);
    }
    void FixedUpdate()
    {
        var cols = Physics2D.OverlapBoxAll(this.transform.position, checkSize, 0, layerToDetect);
        if (cols.Length > 0)
        {
            chip.InitiateDeadSequence();
            this.enabled = false;
        }
    }
}
