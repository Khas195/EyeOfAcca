using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Ripple : SingletonMonobehavior<Ripple>
{
    [SerializeField]
    [Required]
    Material rippleMat = null;
    public float MaxAmount = 50f;

    [Range(0, 1)]
    public float Friction = .9f;

    private float Amount = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }


    void Update()
    {
        this.rippleMat.SetFloat("_Amount", this.Amount);
        this.Amount *= this.Friction;
    }

    public void RippleAt(float x, float y, float friction = 0.9f, float amount = 10)
    {
        this.Amount = this.MaxAmount;
        this.rippleMat.SetFloat("_CenterX", x);
        this.rippleMat.SetFloat("_CenterY", y);
        this.Amount = amount;
        this.Friction = friction;
    }


    public void RippleAt(Vector2 location, float friction = 0.9f, float amount = 10f)
    {
        RippleAt(location.x, location.y, friction, amount);
    }

}
