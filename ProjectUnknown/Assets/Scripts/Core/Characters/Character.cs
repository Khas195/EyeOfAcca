using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class DeathEvent : UnityEvent<Character>
{

}
public class Character : MonoBehaviour
{
    #region Properties
    /// <summary>
    /// The RigidBody of the Character's model. <br/>
    /// Need to be set in Unity Editor.
    /// </summary>
    [SerializeField]
    [BoxGroup("Requirements")]
    [Required]
    protected Rigidbody hostRigidBody;



    /// <summary>
    /// Reference to the movement behavior of the character.<br/>
    /// If the character does not have a movement behavior, he/she will not be able to move.
    /// </summary>
    [SerializeField]
    [BoxGroup("Requirements")]
    [Required]
    protected IMovement movementBehavior;
    /// <summary>
    /// The player character's stats
    /// </summary>
    [Space]
    [SerializeField]
    [BoxGroup("Character Stats Holder")]
    [Required]
    [DisplayScriptableObjectProperties]

    protected CharacterData characterData = null;
    [Space]
    [SerializeField]
    [BoxGroup("Character Stats Holder")]
    [Required]
    [DisplayScriptableObjectProperties]
    protected MovementData moveData = null;



    [SerializeField]
    [BoxGroup("Character Stats Holder")]
    [ReadOnly]
    protected float health = 0;
    [SerializeField]
    [BoxGroup("Character Stats Holder")]
    [ReadOnly]
    protected float stamina = 0;
    [Space]
    /// <summary>
    /// This is event is called when an attack is successfully trigger.
    /// </summary>
    [SerializeField]
    [BoxGroup("Character EVents")]
    public UnityEvent onCharacterAttack = new UnityEvent();

    [SerializeField]
    [BoxGroup("Character EVents")]
    public DeathEvent OnCharacterDeath = new DeathEvent();
    [SerializeField]
    [BoxGroup("Character EVents")]
    public UnityEvent OnCharacterRevived = new UnityEvent();

    [SerializeField]
    [BoxGroup("Character EVents")]
    public UnityEvent OnCharacterDamaged = new UnityEvent();

    [Space]
    /// <summary>
    /// An Scriptable Conditions checker that can be created in the Unity Editor.
    /// jumpConditions check whether it is possible to jump.
    /// </summary>
    [SerializeField]
    [BoxGroup("Character conditions check for actions")]
    [Required]
    protected ConditionsChecker jumpConditions = null;



    /// <summary>
    /// An Scriptable Conditions checker that can be created in the Unity Editor.
    /// moveConditions check whether it is possible to move
    /// </summary>
    [SerializeField]
    [BoxGroup("Character conditions check for actions")]
    [Required]
    protected ConditionsChecker moveConditions;
    /// <summary>
    /// An Scriptable Conditions checker that can be created in the Unity Editor.
    ///  attackConditions check wheter the character can attack.
    /// </summary>
    [SerializeField]
    [BoxGroup("Character conditions check for actions")]
    [Required]
    protected ConditionsChecker attackConditions = null;
    /// <summary>
    /// An Scriptable Conditions checker that can be created in the Unity Editor.
    /// changeMoveTypeConditions check whether it is possible to change the current move mode. 
    /// </summary>
    [SerializeField]
    [BoxGroup("Character conditions check for actions")]
    [Required]
    protected ConditionsChecker changeMoveTypeConditions = null;
    [SerializeField]
    [BoxGroup("Character conditions check for actions")]
    [Required]
    protected ConditionsChecker canRotate = null;


    [SerializeField]
    [BoxGroup("Particle Effect Group")]
    bool playPlayerLandEffect = false;
    [SerializeField]
    [BoxGroup("Particle Effect Group")]
    [ShowIf("playPlayerLandEffect")]
    Transform playerLandParticleSpawnPos = null;
    bool isGround = true;

    private bool rotationLock;
    private bool movementLock;
    private bool jumpLock;



    #endregion
    #region Functions
    #region UnityFunctions
    public virtual void Awake()
    {
        movementBehavior.SetRigidBody(hostRigidBody);
        movementBehavior.SetMovementData(moveData);
        health = characterData.health;
        stamina = characterData.stamina;
    }
    public GameObject GetHost()
    {
        return hostRigidBody.gameObject;
    }
    #endregion
    #region Stats Manipulation
    /// <summary>
    /// Is called if the character are to be damaged.
    /// </summary>
    /// <param name="damage"> the damage value</param>
    public virtual void BeingDamage(int damage)
    {
        this.health -= damage;
        OnCharacterDamaged.Invoke();
        LogHelper.GetInstance().Log(this + " suffered " + damage + ", OUCH!! - Health Left: " + this.health + " out of " + characterData.health);
        if (this.health <= 0)
        {
            OnCharacterDeath.Invoke(this);
        }
    }
    protected virtual void Update()
    {
        if (playPlayerLandEffect)
        {
            HandlePlayerLandParticleEffect();
        }

    }

    private void HandlePlayerLandParticleEffect()
    {
        if (isGround == false)
        {
            if (IsTouchingGround() == true)
            {
                var vfx = VFXSystem.GetInstance();
                if (vfx)
                {
                    vfx.PlayEffect(VFXResources.VFXList.PlayerLand, playerLandParticleSpawnPos.position, Quaternion.Euler(90, 0, 0));
                }
            }
        }
        isGround = IsTouchingGround();
    }

    public float GetHealth()
    {
        return health;
    }
    public float GetStamina()
    {
        return stamina;
    }
    public float GetCurrentSpeed()
    {
        return movementBehavior.GetCurrentSpeed();
    }
    public CharacterData GetCharacterDataPack()
    {
        return characterData;
    }
    #endregion
    #region Action 
    public void RotateToward(Vector3 direction, bool rotateY)
    {
        if (canRotate.IsSatisfied(this) == false || rotationLock == true)
        {
            LogHelper.GetInstance().Log("Rotate conditions not satisfied for " + this);
            return;
        }

        movementBehavior.RotateToward(direction, rotateY);

    }
    /// <summary>
    /// This fucntion ask the moveBehavior of the character to move the character's model.
    /// </summary>
    /// <param name="forward"> fordward is how much the host game object should move forward and backward </param>
    /// <param name="side"> side is how much the host game object should move sideway </param>
    public virtual bool RequestMove(float forward, float side)
    {
        var result = true;
        if (moveConditions.IsSatisfied(this) == false || movementLock == true)
        {
            LogHelper.GetInstance().Log("Movement conditions not satisfied for " + this);
            forward = side = 0;
            result = false;
        }

        movementBehavior.Move(forward, side);
        return result;
    }
    /// <summary>
    /// This function invoke an event whenever the character is signal to attack
    /// It returns true if the event is successfully called and vice versa.
    /// </summary>
    /// <returns></returns>
    public virtual bool Attack()
    {
        if (attackConditions.IsSatisfied(this) == false)
        {

            LogHelper.GetInstance().Log("Attack conditions not satisfied for " + this);
            return false;
        }
        onCharacterAttack.Invoke();

        return true;
    }

    /// <summary>
    /// This function ask the moveBehavior of the character to signal the jump function in the next fixed update;.
    /// </summary>
    /// <returns>
    /// True: if successfully called the SignalJump function and vice versa <br />
    /// </returns>
    public virtual bool RequestJump()
    {
        if (jumpConditions.IsSatisfied(this) && jumpLock == false)
        {
            LogHelper.GetInstance().Log("Jump conditions satisfied for " + this);
            movementBehavior.SignalJump();
            return true;
        }

        LogHelper.GetInstance().Log("Jump conditions not satisfied for " + this);
        return false;
    }
    /// <summary>
    /// This function set the movement Type for the moveBehavior.
    /// </summary>
    /// <param name="moveType"> The movement Type defined in Movement</param>
    /// <returns> 
    /// True: if successfully set the movement mode of the move behavior. <br/>
    /// False: if movementBehavior is null.
    /// </returns>
    public virtual bool RequestMovementType(Movement.MovementType moveType)
    {
        if (movementBehavior.GetCurrentMoveMode() == moveType)
        {
            return true;
        }

        if (changeMoveTypeConditions.IsSatisfied(this))
        {
            movementBehavior.SetMovementMode(moveType);
            return true;
        }

        return false;
    }

    public bool IsTouchingGround()
    {
        return this.movementBehavior.IsTouchingGround();
    }
    public void SetLockRotation(bool lockRot)
    {

        rotationLock = lockRot;
    }
    public bool IsRotationLock()
    {
        return rotationLock;
    }
    public void SetLockMovement(bool lockMov)
    {
        movementLock = lockMov;
    }
    public bool IsMovementLock()
    {
        return movementLock;
    }
    public void SetLockJump(bool lockJump)
    {
        jumpLock = lockJump;
    }
    public bool GetJumpLock()
    {
        return jumpLock;
    }
    public bool IsAlive()
    {
        return health > 0;
    }
    public virtual void IncreaseHealth(float amount)
    {
        this.health += amount;
        if (this.health >= characterData.health)
        {
            this.health = characterData.health;
        }
    }
    public void Revive()
    {
        this.health = characterData.health;
        this.stamina = characterData.stamina;
        OnCharacterRevived.Invoke();
    }
    #endregion


    #endregion
}

