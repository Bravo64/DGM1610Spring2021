using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ParentToTrigger2D : MonoBehaviour
{
    public bool applyToParent = false;
    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (applyToParent)
        {
            transform.parent.parent = other.transform;
        }
        else
        {
            transform.parent = other.transform;
        }
    }
}
