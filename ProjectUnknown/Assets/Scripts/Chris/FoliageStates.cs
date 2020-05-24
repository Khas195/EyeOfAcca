using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoliageStates : MonoBehaviour
{
    [SerializeField]
    private Animator myAnimator;

    [SerializeField]
    private ParticleSystem cutParticles;
    [SerializeField]
    private Transform particleSpawnpoint;

    //Determine which vine sprite set to use (from three)
    private void Awake()
    {
        this.myAnimator.SetInteger("EntryState", Random.Range(0,3));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.G))
        {
            this.TriggerCut();
        }*/
    }

    //Triggers a sway state depending on the direction: less than zero trigger a left sway, triggers right sway otherwise
    public void TriggerSway(int dir)
    {
        if (dir < 0)
        {
            this.myAnimator.SetTrigger("SwayL");
        }
        else
        {
            this.myAnimator.SetTrigger("SwayR");
        }
    }

    //Triggers the cut state
    public void TriggerCut()
    {
        this.myAnimator.SetTrigger("Cut");
        Instantiate(this.cutParticles, this.particleSpawnpoint);
    }
}
