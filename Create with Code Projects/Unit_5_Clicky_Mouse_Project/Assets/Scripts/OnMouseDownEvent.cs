using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnMouseDownEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent onMouseDownEvent;

    private void OnMouseDown()
    {
        onMouseDownEvent.Invoke();
    }
}
