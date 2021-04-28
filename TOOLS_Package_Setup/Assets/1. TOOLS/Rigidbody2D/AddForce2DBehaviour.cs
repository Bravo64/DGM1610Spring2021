using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AddForce2DBehaviour : MonoBehaviour
{
    public enum Directions { X, Y }
    public enum DirectionTypes { Global, Local}
    public enum Modes { OnStart, OnCallOnly, ConstantForce }
    
    public float amount;
    public Directions alongAxis = Directions.X;
    public DirectionTypes directionType = DirectionTypes.Global;
    public Modes mode = Modes.ConstantForce;

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
