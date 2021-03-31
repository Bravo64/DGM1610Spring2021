using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using GameEvents;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] 
    private float movementSpeed = 20.0f;
    [SerializeField] 
    private float gravity = -9.81f;
    [SerializeField] 
    private Transform minXMarker, maxXMarker, playerFirePosition;
    [SerializeField]
    private GameObject[] foodObjectPool;
    [SerializeField]
    private int scoreAtStart = 0;
    [SerializeField]
    private IntData scoreObj;
    [SerializeField]
    private VoidEvent updateScoreText;
    [SerializeField]
    private int livesAtStart = 3;
    [SerializeField]
    private IntData livesObj;
    [SerializeField]
    private VoidEvent updateLivesText;

    private CharacterController _myCharacterController;
    private Animator _myAnimator;
    private float _horizontalInput;
    private Vector3 _movement, _myPosition;
    private int _foodIndex;

    void Start()
    {
        livesObj.value = livesAtStart;
        updateLivesText.Raise();
        scoreObj.value = scoreAtStart;
        updateScoreText.Raise();
        _myCharacterController = GetComponent<CharacterController>();
        _myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        _myPosition = transform.position;
        if (Input.GetButton("Horizontal"))
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            if (_horizontalInput < 0)
            {
                transform.rotation = Quaternion.LookRotation(-Vector3.right);
                if (transform.position.x <= minXMarker.position.x)
                {
                    _horizontalInput = 0;
                }
            }
            else if (_horizontalInput > 0)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.right);
                if (transform.position.x >= maxXMarker.position.x)
                {
                    _horizontalInput = 0;
                }
            }
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward);
            _horizontalInput = 0;
        }

        _movement.Set(_horizontalInput * movementSpeed, gravity, 0);
        _myAnimator.SetFloat("Speed_f",  Mathf.Abs(_horizontalInput * movementSpeed));
        _myCharacterController.Move(_movement * Time.deltaTime);

        if (Input.GetButtonDown("Jump"))
        {
            _myAnimator.SetInteger("Animation_int", 10);
            for (int i = 0; i < 50; i++)
            {
                _foodIndex = Random.Range(0, foodObjectPool.Length);
                if (!foodObjectPool[_foodIndex].activeSelf)
                {
                    foodObjectPool[_foodIndex].transform.position = playerFirePosition.position;
                    foodObjectPool[_foodIndex].SetActive(true);
                    break;
                }
            }
            
        }

        if (Input.GetButtonUp("Jump"))
        {
            _myAnimator.SetInteger("Animation_int", 0);
        }

        _myPosition.Set(transform.position.x, _myPosition.y, _myPosition.z);
        transform.position = _myPosition;
    }
}
