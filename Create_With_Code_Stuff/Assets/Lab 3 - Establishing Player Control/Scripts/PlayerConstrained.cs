using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerConstrained : MonoBehaviour
{
    [SerializeField] 
    private float movementSpeed = 10.0f;
    [SerializeField] 
    private float gravity = -9.81f;
    [SerializeField]
    private float jumpForce = 175f;
    [SerializeField]
    private Transform minXMarker;
    [SerializeField]
    private Transform maxXMarker;
    [SerializeField]
    private Transform minZMarker;
    [SerializeField]
    private Transform maxZMarker;
    
    private CharacterController _myCharacterController;
    private Vector3 _moveDirection;
    private float yDirection, _horizontalInput, _verticalInput;

    void Start()
    {
        _myCharacterController = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        
        Vector3 myPosition = transform.position;
        if (myPosition.x < minXMarker.position.x && _horizontalInput < 0)
        {
            _horizontalInput = 0;
        }
        else if (myPosition.x > maxXMarker.position.x && _horizontalInput > 0)
        {
            _horizontalInput = 0;
        }
        
        if (myPosition.z < minZMarker.position.z && _verticalInput < 0)
        {
            _verticalInput = 0;
        }
        else if (myPosition.z > maxZMarker.position.z && _verticalInput > 0)
        {
            _verticalInput = 0;
        }
        
        _moveDirection.Set(movementSpeed * _horizontalInput, yDirection, movementSpeed * _verticalInput);

        yDirection += gravity * Time.deltaTime;
        
        if (_myCharacterController.isGrounded && _moveDirection.y < 0)
        {
            yDirection = -1;
        }

        if (Input.GetButtonDown("Jump"))
        {
            yDirection = jumpForce;
        }
        
        _moveDirection = transform.TransformDirection(_moveDirection);
        _myCharacterController.Move(_moveDirection * Time.deltaTime);
    }
}
