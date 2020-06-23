using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableIndicatorSwitch : MonoBehaviour
{
    [SerializeField]
    private Sprite activeSprite;

    [SerializeField]
    private ParticleSystem myParticles;

    private SpriteRenderer mySpriteRenderer;

    private bool bActive = false;

    // Start is called before the first frame update
    void Awake()
    {
        this.mySpriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    public void IndicatorActivate()
    {
        if (this.bActive) return;

        this.mySpriteRenderer.sprite = this.activeSprite;
        this.myParticles.Play();
        this.bActive = true;
    }
}
