using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (EntityMove))]

public class MoveCharacter : MonoBehaviour
{
    enum DIRECTON
    {
        LEFT = -1,
        RIGHT = 1
    }

    private DIRECTON facing;

    private EntityMove myMover;

    private Vector2 inputDirection;

    [SerializeField]
    private float jumpForce;

    private GroundChecker myGrounder;

    private void Awake()
    {
        this.myMover = this.GetComponent<EntityMove>();
        this.myGrounder = this.GetComponent<GroundChecker>();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.facing = DIRECTON.LEFT;

        this.myMover.SetBDrag(true);

        this.inputDirection = Vector2.zero;

    }

    // Update is called once per frame
    void Update()
    {
        //Get input on horizontal axis; store in Vector2
        inputDirection = Vector2.right * Input.GetAxisRaw("Horizontal");

        //If Jump Key is pressed
        if (Input.GetKeyDown(ControlKeys.KEY_JUMP))
        {
            //if grounded, Jump upwards
            if (myGrounder.GetIsGrounded())
            {
                myMover.AddPulse(Vector2.up, jumpForce);
            }
        }

        myMover.SetDragTo(myGrounder.GetIsGrounded());

    }

    private void FixedUpdate()
    {
        if (inputDirection.magnitude > 0)
        {
            /*if (inputDirection.x != (int)facing)
            {
                FlipX();
            }*/

            myMover.MoveEntity(inputDirection);

        }
        
    }

    private void FlipX()
    {
        Vector3 scaleTemp = this.transform.localScale;
        scaleTemp.x *= -1;
        this.transform.localScale = scaleTemp;

        this.facing = this.facing == DIRECTON.LEFT ? DIRECTON.RIGHT : DIRECTON.LEFT;
    }

    public void MakeDrag()
    {
        this.myMover.SetBDrag(true);
    }

    public void StopDrag()
    {
        this.myMover.SetBDrag(false);
    }

    public EntityMove GetMover()
    {
        return this.myMover;
    }

    public Vector2 GetInput()
    {
        return this.inputDirection;
    }
}
