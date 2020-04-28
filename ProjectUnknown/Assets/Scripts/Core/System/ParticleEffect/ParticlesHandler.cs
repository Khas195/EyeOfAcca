using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class ParticlesHandler : MonoBehaviour
{
    List<ParticleSystem> particles = new List<ParticleSystem>();

    bool donePlaying = false;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        var particle = this.GetComponent<ParticleSystem>();
        if (particle != null)
        {
            particles.Add(particle);
        }
        particles.AddRange(this.GetComponentsInChildren<ParticleSystem>());
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (donePlaying == false)
        {
            donePlaying = IsAllParticleSystemDonePlaying();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private bool IsAllParticleSystemDonePlaying()
    {
        foreach (var par in particles)
        {
            if (par.isPlaying)
            {
                return false;
            }

        }
        return true;
    }

}
