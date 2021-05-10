using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyDownEventBehavior : MonoBehaviour
{
    public KeyCode key;
    public UnityEvent keyDownEvent;

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            keyDownEvent.Invoke();
        }
    }
}
