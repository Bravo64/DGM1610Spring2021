using System;
using UnityEngine;
using UnityEngine.Events;

public class Trigger2DEventBehavior : MonoBehaviour
{
    public UnityEvent trigger2DEnterEvent;
    private void OnTriggerEnter2D(Collider2D other)
    {
        trigger2DEnterEvent.Invoke();
    }
}
