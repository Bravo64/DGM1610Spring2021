using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoDisableEvent : MonoBehaviour
{
    public UnityEvent onDisableEvent;

    private void OnDisable()
    {
        onDisableEvent.Invoke();
    }
    
}
