using System;
using System.Collections;
using System.Collections.Generic;
using GameEvents;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Animal : MonoBehaviour
{

    [SerializeField]
    private float movementSpeed = 2.0f;
    [SerializeField]
    private float speedIncreasePerSecond = 0.2f;
    [SerializeField]
    private Transform minZMarker;
    [SerializeField]
    private IntData scoreObj;
    [SerializeField]
    private VoidEvent updateScoreText;
    [SerializeField]
    private IntData livesObj;
    [SerializeField]
    private VoidEvent updateLivesText;
    [SerializeField]
    private GameObject disappearParticle;
    
    private Rigidbody _myRigidbody;
    private Vector3 movement;

    private void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        movementSpeed += speedIncreasePerSecond * Time.deltaTime;
        movement.Set(0,0,-movementSpeed);
        _myRigidbody.velocity = movement;
        
        if (transform.position.z < minZMarker.position.z)
        {
            gameObject.SetActive(false);
            livesObj.value--;
            updateLivesText.Raise();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            scoreObj.value++;
            updateScoreText.Raise();
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
