using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Collider2DIgnore : MonoBehaviour
{
    [SerializeField]
    [Tag]
    LayerMask ignoreLayers = 0;
}
