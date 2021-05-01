using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class AdoptChildTrigger : MonoBehaviour
{
    public bool childToMyParent = false;
    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (childToMyParent)
        {
            other.transform.parent = transform.parent;
        }
        else
        {
            other.transform.parent = transform;
        }
    }
}
