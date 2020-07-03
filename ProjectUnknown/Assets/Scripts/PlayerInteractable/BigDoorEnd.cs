using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class BigDoorEnd : MonoBehaviour
{
    [SerializeField]
    [Required]
    BigDoorManager manager = null;
    [SerializeField]
    [Required]
    GameMasterSettings settings = null;
    [SerializeField]
    [Required]
    LevelTransitionDoor doorFunction = null;
    public static UnityEvent OnBigDoorOpenStarted = new UnityEvent();
    public static UnityEvent OnBigDoorOpenFinished = new UnityEvent();
    public static UnityEvent OnSymbolTicked = new UnityEvent();
    float tickTime = 2f;
    float curTickTime = 0.0f;
    int symbolTriggered = 0;
    int tickedAmount = 0;
    bool doOnce = true;
    bool startTicking = false;



    // Start is called before the first frame update
    void Start()
    {
        if (settings.RailUnlocked)
        {
            symbolTriggered++;
        }
        if (settings.GemUnlocked)
        {
            symbolTriggered++;
        }
        if (settings.TimedDoorUnlock)
        {
            symbolTriggered++;
        }
        if (symbolTriggered > 0)
        {
            curTickTime = tickTime;
        }
    }
    void Update()
    {
        if (startTicking == false) return;
        if (tickedAmount < symbolTriggered)
        {
            if (curTickTime >= tickTime)
            {
                tickedAmount += 1;
                curTickTime = 0;
                manager.UpdateSymbols();
                OnSymbolTicked.Invoke();
            }
            curTickTime += Time.deltaTime;
        }
        else
        {
            startTicking = false;
        }

        if (tickedAmount == 3)
        {
            manager.OpenDoor();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            if (doOnce == false) return;
            doOnce = false;
            startTicking = true;
        }
    }
    public void OnBigDoorOpen()
    {
        BigDoorEnd.OnBigDoorOpenStarted.Invoke();
    }
    public void OnBigDoorOpenFished()
    {
        BigDoorEnd.OnBigDoorOpenFinished.Invoke();
        doorFunction.gameObject.SetActive(true);
    }
}
