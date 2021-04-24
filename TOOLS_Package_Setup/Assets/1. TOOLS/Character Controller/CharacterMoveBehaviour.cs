using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMoveBehaviour : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private enum Directions { X, Y, Z }
    private enum DirectionTypes { Global, Local}
    private enum Modes { OnCallOnly, ConstantMovement }
    [SerializeField]
    private Directions alongAxis = Directions.X;
    [SerializeField]
    private DirectionTypes directionType = DirectionTypes.Global;
    [SerializeField]
    private Modes mode = Modes.ConstantMovement;
    private CharacterController _myCharacterController;
    private Vector3 _direction;
    
    void Start()
    {
        _myCharacterController = GetComponent<CharacterController>();

        switch (alongAxis)
        {
            case Directions.X:
                _direction = Vector3.right;
                break;
            case Directions.Y:
                _direction = Vector3.up;
                break;
            case Directions.Z:
                _direction = Vector3.forward;
                break;
            default:
                _direction = Vector3.right;
                break;
        }
        
        if (directionType == DirectionTypes.Local)
        {
            _direction = transform.TransformDirection(_direction);
        }
        
        if (mode == Modes.ConstantMovement)
        {
            StartCoroutine(ConstantlyMoveCharacter());
        }
    }

    public void SetSpeedValue(float inputSpeed)
    {
        speed = inputSpeed;
    }

    public void SetMoveDirection(string inputAxis)
    {
        inputAxis = inputAxis.ToLower();
        
        switch (inputAxis)
        {
            case "x":
                _direction = Vector3.right;
                break;
            case "y":
                _direction = Vector3.up;
                break;
            case "z":
                _direction = Vector3.forward;
                break;
            default:
                _direction = Vector3.right;
                break;
        }
        
        if (directionType == DirectionTypes.Local)
        {
            _direction = transform.TransformDirection(_direction);
        }
    }

    public void MoveCharacter()
    {
        _myCharacterController.Move(_direction * speed * Time.deltaTime);
    }

    IEnumerator ConstantlyMoveCharacter()
    {
        while (true)
        {
            _myCharacterController.Move(_direction * speed * Time.deltaTime);
            yield return 0;
        }
    }
}
