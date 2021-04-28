using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoStartEvent : MonoBehaviour
{
    public UnityEvent onAwakeEvent;
    public UnityEvent onStartEvent;

    private void Awake()
    {
        onAwakeEvent.Invoke();
    }
    
    private void Start()
    {
        onStartEvent.Invoke();
    }
}
