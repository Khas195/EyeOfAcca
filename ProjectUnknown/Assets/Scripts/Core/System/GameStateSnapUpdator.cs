using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class GameStateSnapUpdator : MonoBehaviour
{
    [SerializeField]
    [Required]
    GameStateSnapShot snapShot = null;
    [SerializeField]
    public Camera playerCam = null;
    [SerializeField]
    public Transform characterTran = null;
    [SerializeField]
    public Boomeraxe axe = null;
    [SerializeField]
    public BoomeraxeGrip grip = null;
    // Update is called once per frame
    void Update()
    {
        snapShot.UpdateData(this);
    }
}
