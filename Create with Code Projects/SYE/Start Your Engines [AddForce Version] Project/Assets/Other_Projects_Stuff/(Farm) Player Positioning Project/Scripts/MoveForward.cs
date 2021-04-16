using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveForward : MonoBehaviour
{

    [SerializeField] 
    private float movementSpeed = 50.0f;
    [SerializeField]
    private Transform maxZMarker;
    
    private Rigidbody _myRigidbody;
    private Vector3 movement;

    private void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        movement.Set(0,0,movementSpeed);
        _myRigidbody.velocity = movement;
        
        if (transform.position.z > maxZMarker.position.z)
        {
            gameObject.SetActive(false);
        }
    }
}
