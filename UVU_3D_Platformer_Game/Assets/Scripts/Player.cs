using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] 
    private float movementSpeed = 10.0f;
    [SerializeField] 
    private float gravity = -9.81f;
    [SerializeField]
    private float jumpForce = 175f;
    [SerializeField] 
    private float jumpForceSmoothDecrease = 500.0f;
    [SerializeField] 
    private float mouseTurnSensitivity = 10.0f;
    
    private CharacterController _myCharacterController;
    private Vector3 _moveDirection;
    private float _verticalInput, _horizontalInput, _mouseMovementX;
    private float _currentJumpForce;
    private float _currentGravity;

    void Start()
    {
        _myCharacterController = GetComponent<CharacterController>();
        _currentJumpForce = 0.0f;
    }
    
    void Update()
    {
        if (_currentJumpForce > 0)
        {
            _currentJumpForce -= jumpForceSmoothDecrease * Time.deltaTime;
        }
        _verticalInput = Input.GetAxis("Vertical");
        _horizontalInput = Input.GetAxis("Horizontal");
        _mouseMovementX = Input.GetAxis("Mouse X");
        if (_myCharacterController.isGrounded)
        {
            _currentGravity = gravity;
            if (Input.GetButtonDown("Jump"))
            {
                _currentJumpForce = jumpForce;
            }
        }
        else
        {
            _currentGravity += gravity * 1.5f * Time.deltaTime;
        }
        _moveDirection.Set(_horizontalInput * movementSpeed,
            _currentGravity + _currentJumpForce,
                      _verticalInput * movementSpeed);
        _moveDirection = transform.TransformDirection(_moveDirection);
        _myCharacterController.Move(_moveDirection * Time.deltaTime);
        transform.Rotate(transform.up * (_mouseMovementX * mouseTurnSensitivity));
    }
}
