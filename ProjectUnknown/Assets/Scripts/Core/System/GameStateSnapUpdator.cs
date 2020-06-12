using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class GameStateSnapUpdator : MonoBehaviour
{
    [SerializeField]
    [Required]
    GameStateSnapShot snapShot = null;
    [Required]
    public Camera playerCam = null;
    [Required]
    public Transform characterTran = null;
    [Required]
    public Rigidbody2D characterBody = null;
    [Required]
    public Boomeraxe axe = null;
    [Required]
    public BoomeraxeGrip grip = null;
    // Update is called once per frame
    void Update()
    {
        snapShot.UpdateData(this);
    }
}
