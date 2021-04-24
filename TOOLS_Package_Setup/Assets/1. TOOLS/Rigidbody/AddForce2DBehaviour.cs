using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AddForce2DBehaviour : MonoBehaviour
{
    [SerializeField] 
    private float amount;
    private enum Directions { X, Y }
    private enum DirectionTypes { Global, Local}
    private enum Modes { OnStart, OnCallOnly, ConstantForce }
    [SerializeField]
    private Directions alongAxis = Directions.X;
    [SerializeField]
    private DirectionTypes directionType = DirectionTypes.Global;
    [SerializeField]
    private Modes mode = Modes.ConstantForce;

    private Rigidbody2D _myRigidbody2D;
    private Vector3 _direction;
    
    void Start()
    {
        _myRigidbody2D = GetComponent<Rigidbody2D>();

        switch (alongAxis)
        {
            case Directions.X:
                _direction = Vector3.right;
                break;
            case Directions.Y:
                _direction = Vector3.up;
                break;
            default:
                _direction = Vector3.right;
                break;
        }

        if (mode == Modes.OnStart)
        {
            ApplyForce();
        }
        else if (mode == Modes.ConstantForce)
        {
            if (directionType == DirectionTypes.Global)
            {
                StartCoroutine(ApplyConstantGlobalForce());
            }
            else
            {
                StartCoroutine(ApplyConstantLocalForce());
            }
        }
    }

    public void SetForceAmount(float inputAmount)
    {
        amount = inputAmount;
    }
    
    public void SetForceDirection(string inputAxis)
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
            default:
                _direction = Vector3.right;
                break;
        }
    }
    
    public void SetForceDirectionType(string typeInput)
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

    public void ApplyForce()
    {
        if (directionType == DirectionTypes.Global)
        {
            _myRigidbody2D.AddForce(amount * _direction);
        }
        else
        {
            _myRigidbody2D.AddRelativeForce(amount * _direction);
        }
    }

    IEnumerator ApplyConstantLocalForce()
    {
        while (true)
        {
            _myRigidbody2D.AddForce(amount * _direction);
            yield return 0;
        }
    }
    
    IEnumerator ApplyConstantGlobalForce()
    {
        while (true)
        {
            _myRigidbody2D.AddForce(amount * _direction);
            yield return 0;
        }
    }
}
