using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class RandomEventBehaviour : MonoBehaviour
{
    public bool invokeOnStart = true;
    public UnityEvent[] randomEvents;

    void Start()
    {
        if (invokeOnStart)
        {
            InvokeRandomEvent();
        }
    }

    public void InvokeRandomEvent()
    {
        randomEvents[Random.Range(0, randomEvents.Length)].Invoke();
    }
}
