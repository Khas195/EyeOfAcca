using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemKill : MonoBehaviour
{
    private ParticleSystem myPS;

    private void Awake()
    {
        this.myPS = this.GetComponent<ParticleSystem>();
        if (!this.myPS)
        {
            Debug.LogError("No ParticleSystem found!");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!myPS.isPlaying)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
