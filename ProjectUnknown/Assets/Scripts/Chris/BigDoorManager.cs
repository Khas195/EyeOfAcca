using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BigDoorManager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer myRenderer = null;

    [SerializeField]
    private List<Sprite> stateSprites = new List<Sprite>();

    private int symbolStates = 0;

    private Animator myAnimator = null;

    [SerializeField]
    private List<ParticleSystem> myParticles = new List<ParticleSystem>();
    private void Awake()
    {
        this.symbolStates = 0;
        this.myAnimator = this.GetComponent<Animator>();
    }


    public void UpdateSymbols()
    {
        this.myRenderer.sprite = this.stateSprites[++this.symbolStates];
    }

    public void OpenDoor()
    {
        this.myAnimator.SetTrigger("Open");
        foreach (ParticleSystem ps in this.myParticles)
        {
            ps.Play();
        }
    }

    public void StopParticles()
    {
        foreach (ParticleSystem ps in this.myParticles)
        {
            ps.Stop();
        }
    }


}
