using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class BoomeraxeTeleportator : MonoBehaviour
{
    [SerializeField]
    [Required]
    BoomeraxeGrip grip = null;
    [SerializeField]
    [Required]
    GameObject boomeraxeHolder = null;
    [SerializeField]
    [Required]
    Movement2DPlatform holderMovement = null;
    [SerializeField]
    float airBornTime = 0.0f;

    bool isCountingAirbornTime = false;
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (grip.IsHoldingAxe() == false)
            {
                var pos = grip.GetAxePosition();
                boomeraxeHolder.transform.position = pos;
                var holderBody = boomeraxeHolder.GetComponent<Rigidbody2D>();
                holderBody.gravityScale = 0.0f;
                holderBody.velocity = Vector2.zero;
                holderMovement.gameObject.SetActive(false);

                grip.SetAxeCatchable(true);
                grip.HoldAxe();

                if (isCountingAirbornTime == false)
                {
                    StartCoroutine(TurnBackHolderGravity(airBornTime, holderBody, holderMovement));
                }
            }
        }
    }
    IEnumerator TurnBackHolderGravity(float time, Rigidbody2D holderBody, Movement2DPlatform holderMovement)
    {
        isCountingAirbornTime = true;
        yield return new WaitForSeconds(time);
        holderBody.gravityScale = 1.0f;
        holderMovement.gameObject.SetActive(true);
        isCountingAirbornTime = false;
    }
}
