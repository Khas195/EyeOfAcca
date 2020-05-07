using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private BoxCollider2D myCollider;
    private bool bGrounded;

    [SerializeField]
    private LayerMask groundLayer;

    private Vector2[] groundOverlapCheck;

    private void Awake()
    {
        this.myCollider = this.GetComponent<BoxCollider2D>();
        this.groundOverlapCheck = new Vector2[] { new Vector2(this.myCollider.bounds.min.x + 0.025f, this.myCollider.bounds.min.y),
                                                  new Vector2(this.myCollider.bounds.max.x - 0.025f, this.myCollider.bounds.min.y - (0.025f * this.myCollider.size.y)) };
    }

    // Start is called before the first frame update
    void Start()
    {
        this.bGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(new Vector3(this.myCollider.bounds.min.x + 0.025f, this.myCollider.bounds.min.y, 0),
                       new Vector3(this.myCollider.bounds.max.x - 0.025f, this.myCollider.bounds.min.y - (0.025f * this.myCollider.size.y), 0f),
                       Color.red);
    }

    public bool GetIsGrounded()
    {
        this.bGrounded = Physics2D.OverlapArea(new Vector2(this.myCollider.bounds.min.x + 0.025f, this.myCollider.bounds.min.y),
                                               new Vector2(this.myCollider.bounds.max.x - 0.025f, this.myCollider.bounds.min.y - (0.025f * this.myCollider.size.y)),
                                               this.groundLayer);

        return this.bGrounded;
    }

}
