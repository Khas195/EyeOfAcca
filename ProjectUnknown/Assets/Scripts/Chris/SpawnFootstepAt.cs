using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFootstepAt : MonoBehaviour
{
    [SerializeField]
    private Transform locationA = null;
    [SerializeField]
    private Transform locationB = null;

    [SerializeField]
    private ParticleSystem footstepParticles = null;

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
