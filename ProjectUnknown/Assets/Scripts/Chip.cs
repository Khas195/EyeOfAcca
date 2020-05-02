using UnityEngine;
using UnityEngine.Events;

public class Chip : MonoBehaviour
{
    public Character2D character;

    public UnityEvent axeThrowEvent = new UnityEvent();

    public void AxeThrowEvent()
    {
        axeThrowEvent.Invoke();
    }
}
