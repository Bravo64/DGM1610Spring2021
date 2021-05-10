using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterFullInputBehaviour : MonoBehaviour
{
    public Vector3Data playerPositionObj;
    public bool doubleJumpEnabled = true;
    public float movementSpeed, gravity, jumpForce, mouseTurnSensitivity;

    private CharacterController _myCharacterController;
    private Vector3 _moveDirection;
    private float _yDirection;
    private bool _doubleJumpUsed = false;

    void Start()
    {
        _myCharacterController = GetComponent<CharacterController>();
        
        if (!doubleJumpEnabled)
        {
            _doubleJumpUsed = true;
        }
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

            if (Input.GetButtonDown("Jump"))
            {
                _yDirection = jumpForce;
            }
            
            if (doubleJumpEnabled)
            {
                _doubleJumpUsed = false;
            }
        }
        else if (Input.GetButtonDown("Jump") && !_doubleJumpUsed)
        {
            _yDirection = jumpForce;
            _doubleJumpUsed = true;
        }
        
        _moveDirection = transform.TransformDirection(_moveDirection);
        _myCharacterController.Move(_moveDirection * Time.deltaTime);
        transform.Rotate(transform.up * (Input.GetAxis("Mouse X") * mouseTurnSensitivity));
        playerPositionObj.value = transform.position;
    }
}
