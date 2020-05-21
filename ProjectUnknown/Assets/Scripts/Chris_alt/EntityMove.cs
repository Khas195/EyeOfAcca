using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class EntityMove : MonoBehaviour
{
    private Rigidbody2D myRigidbody = null;

    [SerializeField]
    private float movementSpeed = 0;
    [SerializeField]
    private float acceleration = 0;
    [SerializeField]
    private float dragGround = 0;
    [SerializeField]
    private float dragAir = 0;

    private float drag = 0;

    private bool bDrag = false;

    // Start is called before the first frame update
    void Start()
    {
        this.myRigidbody = this.GetComponent<Rigidbody2D>();
        this.drag = this.dragGround;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(myRigidbody.velocity);
    }

    private void FixedUpdate()
    {
        if (this.bDrag && this.myRigidbody.velocity.x != 0)
        {
            DragVelocityX();
        }
    }

    private void DragVelocity()
    {
        if (this.myRigidbody.velocity.magnitude <= this.drag)
        {
            this.myRigidbody.velocity = Vector2.zero;
        }

        this.myRigidbody.velocity += (-1) * this.myRigidbody.velocity.normalized * this.drag;
    }

    private void DragVelocityX()
    {
        if (Mathf.Abs(this.myRigidbody.velocity.x) <= this.drag)
        {
            this.myRigidbody.velocity = new Vector2(0f, this.myRigidbody.velocity.y);
            return;
        }

        this.myRigidbody.velocity += (-1) * Mathf.Sign(this.myRigidbody.velocity.x) * drag * Vector2.right;
    }

    public void MoveEntity(Vector2 direction)
    {
        this.myRigidbody.velocity += direction.normalized * this.acceleration;

        if (this.myRigidbody.velocity.magnitude > this.movementSpeed)
        {
            this.myRigidbody.velocity = new Vector2(direction.x * this.movementSpeed, this.myRigidbody.velocity.y);
        }

    }

    public void AddPulse(Vector2 direction, float amplitude)
    {
        this.myRigidbody.velocity += direction.normalized * amplitude;
    }

    public void AddPulseAbsolute(Vector2 direction, float amplitude)
    {
        this.myRigidbody.velocity = direction.normalized * amplitude;
    }

    public void SetBDrag(bool bV)
    {
        this.bDrag = bV;
    }

    public bool GetBDrag()
    {
        return this.bDrag;
    }

    public void SetDragTo(bool bGround)
    {
        this.drag = bGround ? this.dragGround : this.dragAir;
    }

    public Rigidbody2D GetMyRigidBody()
    {
        return this.myRigidbody;
    }
}
