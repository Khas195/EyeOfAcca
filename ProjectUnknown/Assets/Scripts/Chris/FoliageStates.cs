using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FoliageStates : AxeInteractable
{
    [SerializeField]
    private Animator myAnimator = null;

    [SerializeField]
    private ParticleSystem cutParticles = null;
    [SerializeField]
    private Transform particleSpawnpoint = null;

    //Determine which vine sprite set to use (from three)
    private void Awake()
    {
        this.myAnimator.SetInteger("EntryState", Random.Range(0, 3));
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

    public override void OnAxeHit(Boomeraxe axe)
    {
        base.OnAxeHit(axe);
        this.TriggerCut();
    }

    public override void OnAxeAbilityTriggered(AxeAbility triggeredAbility)
    {
        base.OnAxeAbilityTriggered(triggeredAbility);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.tag.Equals("Player"))
        {
            var playerBody = other.gameObject.GetComponent<Rigidbody2D>();
            if (playerBody.velocity.x > 0)
            {
                this.TriggerSway(1);
            }
            else
            {
                this.TriggerSway(-1);
            }
        }
    }

}
