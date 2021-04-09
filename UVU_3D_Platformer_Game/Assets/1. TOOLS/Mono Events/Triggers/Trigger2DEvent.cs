using System;
using UnityEngine;
using UnityEngine.Events;

public class Trigger2DEvent : MonoBehaviour
{
    [SerializeField] UnityEvent triggerEnter2DEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        triggerEnter2DEvent.Invoke();
    }
}
