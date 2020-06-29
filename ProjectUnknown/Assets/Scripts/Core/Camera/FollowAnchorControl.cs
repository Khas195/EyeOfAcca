using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class FollowAnchorControl : MonoBehaviour
{
    [SerializeField]
    [Required]
    Transform followAnchor = null;
    [SerializeField]
    [Required]
    CameraFollow follow = null;
    [SerializeField]
    [Required]
    GameStateSnapShot snap = null;
    [SerializeField]
    [Required]
    Transform followPoint = null;
    [SerializeField]
    [Required]
    CameraSettings cameraSettings = null;
    [SerializeField]
    [ReadOnly]
    float curLookDownHoldTime = 0;
    [SerializeField]
    [ReadOnly]
    Vector3 cachedPos = Vector3.zero;
    [SerializeField]
    [ReadOnly]
    Vector3 targetLead = Vector3.one;
    void Start()
    {
        cachedPos = followPoint.localPosition;
    }

    public void FollowCharacter()
    {
        this.followAnchor.transform.position = snap.CharacterPosition;
    }
    public void UpdateAnchor()
    {
        var pos = followPoint.localPosition;
        pos.x = Mathf.Lerp(pos.x, targetLead.x, cameraSettings.leadSpeed * Time.deltaTime);
        followPoint.localPosition = pos;
    }
    public void Reset()
    {
        targetLead = cachedPos;
        SetY(cachedPos.y);
    }

    public void HandleLeading()
    {
        if (follow.IsHoningX())
        {
            if (snap.CharacterVelocity.x > 0)
            {
                targetLead.x = cameraSettings.leadDistance;
            }
            else if (snap.CharacterVelocity.x < 0)
            {
                targetLead.x = cameraSettings.leadDistance * -1;
            }
        }
        else
        {
            targetLead.x = cachedPos.x;
        }
    }
    public void HandleLookDown()
    {
        if (Input.GetKey(KeyCode.S))
        {
            curLookDownHoldTime += Time.deltaTime;
        }
        else
        {
            curLookDownHoldTime = 0;
        }

        if (curLookDownHoldTime > cameraSettings.lookDownHoldTIme)
        {
            LookDown();
        }
        else
        {
            SetY(cachedPos.y);
        }
    }
    private void LookDown()
    {
        SetY(cameraSettings.lookDownDistance * -1);
    }
    public void SetY(float value)
    {
        var pos = followPoint.localPosition;
        pos.y = value;
        followPoint.localPosition = pos;

    }
}
