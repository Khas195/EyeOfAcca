using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    [SerializeField]
    List<TransitionCurve> transitions = new List<TransitionCurve>();

#if UNITY_EDITOR
    [Button("Find Transitions In Children")]
    public void FindTransitionsInChildren()
    {
        transitions.Clear();
        var foundTransitions = this.GetComponentsInChildren<TransitionCurve>();
        transitions.AddRange(foundTransitions);
    }
#endif

    public void TransitionIn()
    {
        for (int i = 0; i < transitions.Count; i++)
        {
            transitions[i].TransitionIn();
        }
    }
    public void TransitionOut()
    {
        for (int i = 0; i < transitions.Count; i++)
        {
            transitions[i].TransitionOut();
        }
    }
}
