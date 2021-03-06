using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Door : MonoBehaviour
{
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    GameMasterSettings settings = null;
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    SpriteRenderer render = null;
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    Sprite normal = null;
    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    Sprite locked = null;


    [BoxGroup("Requirements")]
    [SerializeField]
    [Required]
    MoveAB moveAB = null;
    [BoxGroup("Requirements")]
    [SerializeField]
    [ValidateInput("IsNotMiddle", "Open position cannot be in the middle")]
    [OnValueChanged("CheckIsOpen")]
    MoveAB.MoveABEnum OpenPos = MoveAB.MoveABEnum.A;

    [BoxGroup("Optional")]
    [SerializeField]
    Transform particleSpawnPoint = null;

    [BoxGroup("Current Status")]
    [SerializeField]
    [ReadOnly]
    bool isOpen = false;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        CheckIsOpen();
    }
    void Start()
    {
        UpdateDoorSprite();
        settings.OnTimedDoorUnlock.AddListener(OnTimedDoorUnlock);
    }

    private void OnTimedDoorUnlock()
    {
        UpdateDoorSprite();
        VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.TimedDoorUnlock, this.render.gameObject.transform.position, Quaternion.identity);
    }

    private void UpdateDoorSprite()
    {
        if (settings.TimedDoorUnlock)
        {
            render.sprite = normal;
        }
        else
        {
            render.sprite = locked;
        }
    }

    public bool IsNotMiddle(MoveAB.MoveABEnum value)
    {
        return value != MoveAB.MoveABEnum.Middle;
    }

    public void CheckIsOpen(MoveAB.MoveABEnum old, MoveAB.MoveABEnum newValue)
    {
        CheckIsOpen();
    }


    public bool CheckIsOpen()
    {
        if (moveAB.IsAt(MoveAB.MoveABEnum.A) && OpenPos == MoveAB.MoveABEnum.A || moveAB.IsAt(MoveAB.MoveABEnum.B) && OpenPos == MoveAB.MoveABEnum.B)
        {
            isOpen = true;
        }
        else
        {
            isOpen = false;
        }
        return isOpen;
    }

    /// <summary>
    /// Sent each frame where a collider on another object is touching
    /// this object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Ground"))
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), other.collider);
            return;
        }
    }


    public void SpawnParticle()
    {
        var effect = VFXSystem.GetInstance().PlayEffect(VFXResources.VFXList.DoorCloseDust, particleSpawnPoint.transform.position, Quaternion.identity);
        effect.transform.parent = particleSpawnPoint.transform;
    }
    [Button("Switch")]
    public void TriggerMechanism()
    {
        if (isOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    [Button("Open")]
    public void Open()
    {
        if (settings.TimedDoorUnlock == false) return;
        moveAB.GoTo(OpenPos);
        isOpen = true;
    }

    [Button("Close")]
    public void Close()
    {
        if (settings.TimedDoorUnlock == false) return;
        if (OpenPos == MoveAB.MoveABEnum.A)
        {
            moveAB.GoTo(MoveAB.MoveABEnum.B);
            isOpen = false;
        }
        else if (OpenPos == MoveAB.MoveABEnum.B)
        {
            moveAB.GoTo(MoveAB.MoveABEnum.A);
            isOpen = false;
        }
    }
}
