using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningRail : MonoBehaviour
{
    [SerializeField]
    Animator railAnim = null;
    [SerializeField]
    List<RailBlock> railblocks = new List<RailBlock>();
    int numOfActive = 0;

    void Start()
    {
        for (int i = 0; i < railblocks.Count; i++)
        {
            var moveAB = railblocks[i].GetComponent<MoveAB>();
            moveAB.OnReached.AddListener(DecreaseActiveRail);
            railblocks[i].OnBlockMove.AddListener(IncreaseActiveRail);
        }
    }
    void FixedUpdate()
    {
        if (numOfActive > 0)
        {
            Spin();
        }
        else
        {
            StopSpin();
        }
    }
    public void Spin()
    {
        railAnim.SetBool("Spinning", true);
    }

    private void IncreaseActiveRail()
    {
        numOfActive++;
    }

    public void StopSpin()
    {
        railAnim.SetBool("Spinning", false);
    }

    private void DecreaseActiveRail()
    {
        numOfActive--;
    }
}
