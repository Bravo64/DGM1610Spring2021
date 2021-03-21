using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] 
    private float movementSpeed = 5.0f;
    [SerializeField] 
    private float gravity = -9.81f;
    [SerializeField]
    private float jumpForce = 100f;
    [SerializeField] 
    private float mouseTurnSensitivity = 10.0f;
    
    private CharacterController _myCharacterController;
    private Vector3 _moveDirection;
    private float _verticalInput, _horizontalInput, _mouseMovementX;
    
    void Start()
    {
        _myCharacterController = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        _verticalInput = Input.GetAxis("Vertical");
        _horizontalInput = Input.GetAxis("Horizontal");
        _mouseMovementX = Input.GetAxis("Mouse X");
        _moveDirection.Set(_horizontalInput * movementSpeed, 
                      gravity, 
                      _verticalInput * movementSpeed);
        if (Input.GetButtonDown("Jump") && _myCharacterController.isGrounded)
        {
            _moveDirection.y += jumpForce;
        }
        _moveDirection = transform.TransformDirection(_moveDirection);
        _myCharacterController.Move(_moveDirection * Time.deltaTime);
        transform.Rotate(transform.up * (_mouseMovementX * mouseTurnSensitivity));
    }
}
