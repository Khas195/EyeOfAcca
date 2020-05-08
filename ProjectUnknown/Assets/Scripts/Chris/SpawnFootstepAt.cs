using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFootstepAt : MonoBehaviour
{
    [SerializeField]
    private Transform locationA;
    [SerializeField]
    private Transform locationB;

    [SerializeField]
    private ParticleSystem footstepParticles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEm(int i)
    {
        Instantiate(this.footstepParticles, i == 0 ? locationA : locationB);
    }
}
