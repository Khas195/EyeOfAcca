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
    void Start()
    {
        this.mySpriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            IndicatorActivate();
        }
    }

    public void IndicatorActivate()
    {
        if (this.bActive) return;

        this.mySpriteRenderer.sprite = this.activeSprite;
        this.myParticles.Play();
        this.bActive = true;
    }
}
