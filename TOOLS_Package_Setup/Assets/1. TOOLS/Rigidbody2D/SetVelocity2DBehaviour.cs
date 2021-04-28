using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SetVelocity2DBehaviour : MonoBehaviour
{
    public enum Directions { X, Y }
    public enum DirectionTypes { Global, Local}
    public enum Modes { OnStart, OnCallOnly, ConstantMovement }
    
    public float speed;
    public Directions alongAxis = Directions.X;
    public DirectionTypes directionType = DirectionTypes.Global;
    public Modes mode = Modes.ConstantMovement;
    
    private Rigidbody2D _myRigidbody2D;
    private Vector2 _actualDirection;
    private Vector2 _globalDirection;
    
    void Start()
    {
        _myRigidbody2D = GetComponent<Rigidbody2D>();
        
        switch (alongAxis)
        {
            case Directions.X:
                _globalDirection = Vector2.right;
                break;
            case Directions.Y:
                _globalDirection = Vector2.up;
                break;
            default:
                _globalDirection = Vector2.right;
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
        _myRigidbody2D.velocity = _actualDirection * speed;
    }

    IEnumerator SetConstantMovement()
    {
        while (true)
        {
            if (directionType == DirectionTypes.Local)
            {
                _actualDirection = transform.TransformDirection(_globalDirection);
            }
            _myRigidbody2D.velocity = _actualDirection * speed;
            yield return 0;
        }
    }
}