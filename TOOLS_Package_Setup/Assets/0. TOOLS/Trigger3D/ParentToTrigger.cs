using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class ParentToTrigger : MonoBehaviour
{
    public bool applyToParent = false;
    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }
    
    private void OnTriggerEnter(Collider other)
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
