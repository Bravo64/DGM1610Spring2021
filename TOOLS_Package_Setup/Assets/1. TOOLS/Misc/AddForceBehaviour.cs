using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AddForceBehaviour : MonoBehaviour
{
    [SerializeField] 
    private float amount;
    private enum Directions { X, Y, Z }
    private enum DirectionTypes { Global, Local}
    private enum Modes { OnStart, OnCallOnly, ConstantForce }
    [SerializeField]
    private Directions alongAxis = Directions.X;
    [SerializeField]
    private DirectionTypes directionType = DirectionTypes.Global;
    [SerializeField]
    private Modes mode = Modes.ConstantForce;

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
            case "z":
                _direction = Vector3.forward;
                break;
            default:
                _direction = Vector3.right;
                break;
        }
    }

    public void ApplyForce()
    {
        if (directionType == DirectionTypes.Global)
        {
            _myRigidbody.AddForce(amount * _direction);
        }
        else
        {
            _myRigidbody.AddRelativeForce(amount * _direction);
        }
    }

    IEnumerator ApplyConstantLocalForce()
    {
        while (true)
        {
            _myRigidbody.AddForce(amount * _direction);
            yield return 0;
        }
    }
    
    IEnumerator ApplyConstantGlobalForce()
    {
        while (true)
        {
            _myRigidbody.AddForce(amount * _direction);
            yield return 0;
        }
    }
}
