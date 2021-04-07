using System;
using UnityEngine;
using UnityEngine.Events;

public class TriggerExitEvent : MonoBehaviour
{
    public UnityEvent triggerExitEvent;
    private void OnTriggerExit(Collider other)
    {
        triggerExitEvent.Invoke();
    }
}
