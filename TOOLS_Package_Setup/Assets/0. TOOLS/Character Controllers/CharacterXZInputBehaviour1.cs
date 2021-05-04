using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterXZInputBehaviour : MonoBehaviour
{
    public Vector3Data playerPositionObj;
    public float movementSpeed;

    private CharacterController _myCharacterController;
    private Vector3 _moveDirection;

    void Start()
    {
        _myCharacterController = GetComponent<CharacterController>();
    }
    
    void LateUpdate()
    {
        _moveDirection.Set(movementSpeed * Input.GetAxis("Horizontal"), 0, movementSpeed * Input.GetAxis("Vertical"));
        _moveDirection = transform.TransformDirection(_moveDirection);
        _myCharacterController.Move(_moveDirection * Time.deltaTime);
        playerPositionObj.value = transform.position;
    }
}
