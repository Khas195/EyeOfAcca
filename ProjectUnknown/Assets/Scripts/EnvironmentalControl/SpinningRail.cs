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

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(this.transform.position, 0.3f);
        foreach (var item in railblocks)
        {
            Gizmos.DrawLine(this.transform.position, item.transform.position);
            Gizmos.DrawWireSphere(item.transform.position, 0.3f);
        }
    }

    void Start()
    {

    }
    void FixedUpdate()
    {
        bool shouldSpin = false;
        for (int i = 0; i < railblocks.Count; i++)
        {
            var moveAB = railblocks[i].GetComponent<MoveAB>();
            if (moveAB.IsInMotion())
            {
                shouldSpin = true;
                break;
            }
        }
        if (shouldSpin)
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
