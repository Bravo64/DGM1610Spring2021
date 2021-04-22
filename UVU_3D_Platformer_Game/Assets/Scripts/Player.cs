using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] 
    private Vector3Data playerPositionObj;
    [SerializeField] 
    private UnityEvent activateWeapon;
    [SerializeField] 
    private float movementSpeed = 10.0f;
    [SerializeField] 
    private float gravity = -9.81f;
    [SerializeField]
    private float jumpForce = 175f;
    [SerializeField]
    private float mouseTurnSensitivity = 10.0f;
    [SerializeField]
    private float bulletForce = 50.0f;
    [SerializeField] 
    private Transform gunFirePoint;
    [SerializeField]
    private Rigidbody[] bulletPool;
    [SerializeField]
    private StringData activeWeaponObj;
    
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

        if (Input.GetButtonDown("Fire1"))
        {
            if (activeWeaponObj.GetString() == "Sword")
            {
                activateWeapon.Invoke();
            }
            else if (activeWeaponObj.GetString() == "Gun")
            {
                foreach (Rigidbody bullet in bulletPool)
                {
                    if (!bullet.gameObject.activeSelf)
                    {
                        bullet.Sleep();
                        bullet.transform.position = gunFirePoint.position;
                        bullet.transform.rotation = gunFirePoint.rotation;
                        bullet.gameObject.SetActive(true);
                        bullet.WakeUp();
                        bullet.velocity += transform.forward * bulletForce;
                        break;
                    }
                }
            }
        }

        _moveDirection = transform.TransformDirection(_moveDirection);
        _myCharacterController.Move(_moveDirection * Time.deltaTime);
        transform.Rotate(transform.up * (Input.GetAxis("Mouse X") * mouseTurnSensitivity));
        playerPositionObj.value = transform.position;
    }
}
