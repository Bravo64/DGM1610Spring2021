using System;
using UnityEngine;
using UnityEngine.Events;

public class TriggerExit2DEvent : MonoBehaviour
{
    public UnityEvent triggerExit2DEvent;
    private void OnTriggerExit2D(Collider2D other)
    {
        triggerExit2DEvent.Invoke();
    }
}
