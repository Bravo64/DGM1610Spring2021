using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoDisableEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent onDisableEvent;

    private void OnDisable()
    {
        onDisableEvent.Invoke();
    }
    
}
