using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SetVelocityBehaviour : MonoBehaviour
{
    public enum Directions { X, Y, Z }
    public enum DirectionTypes { Global, Local}
    public enum Modes { OnStart, OnCallOnly, ConstantMovement }
    
    public float speed;
    public Directions alongAxis = Directions.X;
    public DirectionTypes directionType = DirectionTypes.Global;
    public Modes mode = Modes.ConstantMovement;
    
    private Rigidbody _myRigidbody;
    private Vector3 _actualDirection;
    private Vector3 _globalDirection;
    
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
        
        switch (alongAxis)
        {
            case Directions.X:
                _globalDirection = Vector3.right;
                break;
            case Directions.Y:
                _globalDirection = Vector3.up;
                break;
            case Directions.Z:
                _globalDirection = Vector3.forward;
                break;
            default:
                _globalDirection = Vector3.right;
                break;
        }

        if (directionType == DirectionTypes.Global)
        {
            _actualDirection = _globalDirection;
        }
        else if (directionType == DirectionTypes.Local)
        {
            _actualDirection = transform.TransformDirection(_globalDirection);
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

    public void SetVelocity()
    {
        if (directionType == DirectionTypes.Local)
        {
            _actualDirection = transform.TransformDirection(_globalDirection);
        }
        _myRigidbody.velocity = _actualDirection * speed;
    }

    IEnumerator SetConstantMovement()
    {
        while (true)
        {
            if (directionType == DirectionTypes.Local)
            {
                _actualDirection = transform.TransformDirection(_globalDirection);
            }
            _myRigidbody.velocity = _actualDirection * speed;
            yield return 0;
        }
    }
}
