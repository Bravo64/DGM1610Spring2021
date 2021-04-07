using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Object : MonoBehaviour
{
    [SerializeField] 
    private ParticleSystem myPararticle;
    private Rigidbody _myRigidbody;
    private float _minSpeed = 11;
    private float _maxSpeed = 14;
    private float _maxTorque = 1;
    private float _xRange = 4;
    private float _ySpawnPos = -1;
    private Vector3 randomPos;
    
    // Start is called before the first frame update
    void Awake()
    {
        _myRigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _myRigidbody.AddForce(RandomForce(), ForceMode.Impulse);
        _myRigidbody.AddTorque(RandomTorque(), RandomTorque(), 
                                RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
    }

    private void OnDisable()
    {
        _myRigidbody.velocity = Vector3.zero;
        _myRigidbody.angularVelocity = Vector3.zero;
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(_minSpeed, _maxSpeed);
    }
    
    private float RandomTorque()
    {
        return Random.Range(-_maxTorque, _maxTorque);
    }
    
    private Vector3 RandomSpawnPos()
    {
        randomPos.Set(Random.Range(-_xRange, _xRange), _ySpawnPos, Random.Range(0.0f, -20.0f));
        return randomPos;
    }

    public void PlayParticle()
    {
        myPararticle.transform.parent = null;
        myPararticle.transform.position = transform.position;
        myPararticle.Play();
    }
}
