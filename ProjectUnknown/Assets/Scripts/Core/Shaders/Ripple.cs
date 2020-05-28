using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Ripple : MonoBehaviour
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

    public void RippleAt(float x, float y)
    {
        this.Amount = this.MaxAmount;
        this.rippleMat.SetFloat("_CenterX", x);
        this.rippleMat.SetFloat("_CenterY", y);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, rippleMat);
    }

    public void RippleAt(Vector2 location)
    {
        var axeScreenPos = this.GetComponent<Camera>().WorldToScreenPoint(location);
        RippleAt(axeScreenPos.x, axeScreenPos.y);
    }
}
