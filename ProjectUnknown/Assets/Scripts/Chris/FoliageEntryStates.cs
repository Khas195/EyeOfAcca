using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoliageEntryStates : MonoBehaviour
{
    [SerializeField]
    Animator myAnimator;

    private void Awake()
    {
        int i = Random.Range(0, 3);
        this.myAnimator.SetInteger("EntryState", i);
        Debug.Log(i);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
