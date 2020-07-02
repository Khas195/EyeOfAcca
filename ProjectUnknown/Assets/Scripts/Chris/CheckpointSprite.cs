using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSprite : MonoBehaviour
{
    private SpriteRenderer myRenderer;

    [SerializeField]
    private Sprite inactiveSprite;
    [SerializeField]
    private Sprite activeSprite;

    [SerializeField]
    private List<ParticleSystem> myParticles;
    private void Awake()
    {
        this.myRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MakeActive()
    {
        this.myRenderer.sprite = this.activeSprite;

        foreach (ParticleSystem ps in this.myParticles)
        {
            ps.Play();
        }
    }

    public void MakeInactive()
    {
        this.myRenderer.sprite = this.inactiveSprite;

        foreach (ParticleSystem ps in this.myParticles)
        {
            ps.Stop();
        }
    }
}
