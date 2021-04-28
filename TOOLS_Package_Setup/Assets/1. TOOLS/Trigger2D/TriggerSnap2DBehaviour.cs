using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class TriggerSnap2DBehaviour : MonoBehaviour
{
    public enum Modes { Enabled, Disabled}

    public bool snapToOtherPosition = true;
    public bool snapToOtherRotation = true;
    public Modes applySnapToParent = Modes.Disabled;
    private Transform _currentTransform;
    
    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
        
        if (applySnapToParent == Modes.Enabled)
        {
            _currentTransform = transform.parent;
        }
        else
        {
            _currentTransform = transform;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (snapToOtherPosition)
        {
            _currentTransform.position = other.transform.position;
        }
        
        if (snapToOtherRotation)
        {
            _currentTransform.rotation = other.transform.rotation;
        }
    }
}
