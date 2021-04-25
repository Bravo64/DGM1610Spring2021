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
