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
    private float yDirection;

    void Start()
    {
        _myCharacterController = GetComponent<CharacterController>();
    }
    
    void LateUpdate()
    {
        _moveDirection.Set(movementSpeed * Input.GetAxis("Horizontal"), yDirection, movementSpeed * Input.GetAxis("Vertical"));

        yDirection += gravity * Time.deltaTime;
        
        if (_myCharacterController.isGrounded && _moveDirection.y < 0)
        {
            yDirection = -5;
        }

        if (Input.GetButtonDown("Jump"))
        {
            yDirection = jumpForce;
        }
        
        _moveDirection = transform.TransformDirection(_moveDirection);
        _myCharacterController.Move(_moveDirection * Time.deltaTime);
        transform.Rotate(transform.up * (Input.GetAxis("Mouse X") * mouseTurnSensitivity));
        playerPositionObj.value = transform.position;
    }
}
