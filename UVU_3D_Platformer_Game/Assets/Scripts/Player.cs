using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] 
    private Vector3Data playerPositionObj;
    [SerializeField] 
    private float movementSpeed = 10.0f;
    [SerializeField] 
    private float gravity = -9.81f;
    [SerializeField]
    private float jumpForce = 175f;
    [SerializeField]
    private float mouseTurnSensitivity = 10.0f;
    
    private CharacterController _myCharacterController;
    private Vector3 _moveDirection;
    private float _yDirection;
    private bool _doubleJumpActivated = false;

    void Start()
    {
        _myCharacterController = GetComponent<CharacterController>();
    }
    
    void LateUpdate()
    {
        _moveDirection.Set(movementSpeed * Input.GetAxis("Horizontal"), _yDirection, movementSpeed * Input.GetAxis("Vertical"));

        _yDirection += gravity * Time.deltaTime;

        if (_myCharacterController.isGrounded)
        {
            if (_moveDirection.y < 0)
            {
                _yDirection = -5;
            }

            if (_doubleJumpActivated)
            {
                _doubleJumpActivated = false;
            }

            if (Input.GetButtonDown("Jump"))
            {
                _yDirection = jumpForce;
            }
        }
        else if (Input.GetButtonDown("Jump") && !_doubleJumpActivated)
        {
            _yDirection = jumpForce;
            _doubleJumpActivated = true;
        }
        
        _moveDirection = transform.TransformDirection(_moveDirection);
        _myCharacterController.Move(_moveDirection * Time.deltaTime);
        transform.Rotate(transform.up * (Input.GetAxis("Mouse X") * mouseTurnSensitivity));
        playerPositionObj.value = transform.position;
    }
}
