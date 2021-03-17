using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using GameEvents;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(CharacterController))]
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
    private float _horizontalInput;
    private Vector3 _movement;
    private int _foodIndex;

    void Start()
    {
        livesObj.value = livesAtStart;
        updateLivesText.Raise();
        scoreObj.value = scoreAtStart;
        updateScoreText.Raise();
        _myCharacterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        if (transform.position.x <= minXMarker.position.x && _horizontalInput < 0)
        {
            _horizontalInput = 0;
        }
        else if (transform.position.x >= maxXMarker.position.x && _horizontalInput > 0)
        {
            _horizontalInput = 0;
        }

        _movement.Set(_horizontalInput * movementSpeed, gravity, 0);
        _myCharacterController.Move(_movement * Time.deltaTime);

        if (Input.GetButtonDown("Jump"))
        {
            for (int i = 0; i < 10; i++)
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
    }
}
