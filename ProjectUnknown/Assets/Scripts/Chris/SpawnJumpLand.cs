using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnJumpLand : MonoBehaviour
{
    [SerializeField]
    public ParticleSystem jumpParticles;
    public ParticleSystem landParticles;

    [SerializeField]
    public Transform particleLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnLandJump(int i)
    {
        Instantiate(i == 0 ? this.jumpParticles : this.landParticles, this.particleLocation);
    }
}
