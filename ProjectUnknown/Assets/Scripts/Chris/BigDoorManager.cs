using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDoorManager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer myRenderer;

    [SerializeField]
    private List<Sprite> stateSprites;

    private int symbolStates;

    private Animator myAnimator;

    [SerializeField]
    private List<ParticleSystem> myParticles;

    private void Awake()
    {
        this.symbolStates = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.myAnimator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            this.OpenDoor();
        }
    }

    public void UpdateSymbols()
    {
        this.myRenderer.sprite = this.stateSprites[++this.symbolStates];
    }

    public void OpenDoor()
    {
        this.myAnimator.SetTrigger("Open");
        foreach (ParticleSystem ps in this.myParticles){
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
