using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource mySource = null;

    [SerializeField]
    private List<AudioClip> footstepClips = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayFootstep()
    {
        this.mySource.PlayOneShot(this.footstepClips[Random.Range(0, this.footstepClips.Count)]);
    }
}
