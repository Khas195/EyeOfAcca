using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Chip : MonoBehaviour
{
    [SerializeField]
    private float timeUntilLevelReload = 0.5f;
    [SerializeField]
    private float deadTimeFreezeAmount = 0.5f;
    [SerializeField]
    private float deadAnglePushBackOffset = 45;
    [SerializeField]
    GameStateSnapShot snap = null;

    [SerializeField]
    UnityEvent OnCharacterDeath = new UnityEvent();
    [SerializeField]
    Animator anim = null;

    [SerializeField]
    [Required]
    GameObject deadAxe = null;

    [SerializeField]
    [Required]
    Rigidbody2D charBody = null;
    [SerializeField]
    float deadAxeSpeed = 0.0f;
    [SerializeField]
    float characterDeadSpeed = 0.0f;

    public void InitiateDeadSequence()
    {
        if (snap != null)
        {
            var charVel = snap.CharacterVelocity;
            if (snap.IsHoldingAxe)
            {
                var axe = GameObject.Instantiate(deadAxe, snap.CharacterPosition, Quaternion.identity, this.transform);
                axe.transform.parent = null;
                var axeBody = axe.GetComponent<Rigidbody2D>();
                axeBody.velocity = RotateVectorByAngle(-deadAnglePushBackOffset, charVel.normalized * -1) * deadAxeSpeed;
            }
            charBody.velocity = RotateVectorByAngle(deadAnglePushBackOffset, charVel.normalized * -1) * characterDeadSpeed;
            Time.timeScale = deadTimeFreezeAmount;

        }
        anim.SetBool("Dead", true);
        OnCharacterDeath.Invoke();
        StartCoroutine(TriggerSceneReloadCoroutine(timeUntilLevelReload));
    }

    private static Vector3 RotateVectorByAngle(float angle, Vector3 dir)
    {
        return Quaternion.Euler(0, 0, angle) * dir;
    }

    public IEnumerator TriggerSceneReloadCoroutine(float time)
    {
        var waitfor = new WaitForSeconds(time);
        yield return waitfor;
        Time.timeScale = 1.0f;

        GameMaster.GetInstance().IncreaseDeadCount();
        GameMaster.GetInstance().RestartLevel();

    }
}
