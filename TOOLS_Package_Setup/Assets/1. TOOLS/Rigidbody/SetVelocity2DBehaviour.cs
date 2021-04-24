using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SetVelocity2DBehaviour : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private enum Directions { X, Y }
    private enum DirectionTypes { Global, Local}
    private enum Modes { OnStart, OnCallOnly, ConstantVelocity }
    [SerializeField]
    private Directions alongAxis = Directions.X;
    [SerializeField]
    private DirectionTypes directionType = DirectionTypes.Global;
    [SerializeField]
    private Modes mode = Modes.ConstantVelocity;
    private Rigidbody2D _myRigidbody2D;
    private Vector2 _direction;
    
    void Start()
    {
        _myRigidbody2D = GetComponent<Rigidbody2D>();

        switch (alongAxis)
        {
            case Directions.X:
                _direction = Vector2.right;
                break;
            case Directions.Y:
                _direction = Vector2.up;
                break;
            default:
                _direction = Vector2.right;
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
        else if (mode == Modes.ConstantVelocity)
        {
            StartCoroutine(SetConstantVelocity());
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
                _direction = Vector2.right;
                break;
            case "y":
                _direction = Vector2.up;
                break;
            default:
                _direction = Vector2.right;
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
        _myRigidbody2D.velocity = _direction * speed;
    }

    IEnumerator SetConstantVelocity()
    {
        while (true)
        {
            _myRigidbody2D.velocity = _direction * speed;
            yield return 0;
        }
    }
}
