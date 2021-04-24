using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SetVelocityBehaviour : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private enum Directions { X, Y, Z }
    private enum DirectionTypes { Global, Local}
    private enum Modes { OnStart, OnCallOnly, ConstantMovement }
    [SerializeField]
    private Directions alongAxis = Directions.X;
    [SerializeField]
    private DirectionTypes directionType = DirectionTypes.Global;
    [SerializeField]
    private Modes mode = Modes.ConstantMovement;
    private Rigidbody _myRigidbody;
    private Vector3 _direction;
    
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();

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

        if (mode == Modes.OnStart)
        {
            SetVelocity();
        }
        else if (mode == Modes.ConstantMovement)
        {
            StartCoroutine(SetConstantMovement());
        }
    }

    public void SetSpeedValue(float inputSpeed)
    {
        speed = inputSpeed;
    }

    public void SetVelocityDirection(string inputAxis)
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
    
    public void SetVelocityDirectionType(string typeInput)
    {
        typeInput = typeInput.ToLower();
        
        switch (typeInput)
        {
            case "global":
                directionType = DirectionTypes.Global;
                break;
            case "local":
                directionType = DirectionTypes.Local;
                break;
            default:
                directionType = DirectionTypes.Global;
                break;
        }
    }
    
    public void SetVelocity()
    {
        _myRigidbody.velocity = _direction * speed;
    }

    IEnumerator SetConstantMovement()
    {
        while (true)
        {
            _myRigidbody.velocity = _direction * speed;
            yield return 0;
        }
    }
}
