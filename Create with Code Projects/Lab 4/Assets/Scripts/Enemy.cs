using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 100.0f;
    [SerializeField]
    private Transform minZMarker;
    
    private Rigidbody _myRigidbody;
    private Vector3 _myVelocity;

    private void Awake()
    {
        _myRigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _myRigidbody.velocity = Vector3.zero;
        _myRigidbody.angularVelocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        _myVelocity = _myRigidbody.velocity;
        _myVelocity.z = -movementSpeed * Time.deltaTime;
        _myRigidbody.velocity = _myVelocity;

        if (transform.position.z < minZMarker.position.z - 3.0f)
        {
            gameObject.SetActive(false);
        }
    }
}
