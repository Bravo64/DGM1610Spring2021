using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class TriggerDisableOther : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
    }
}
