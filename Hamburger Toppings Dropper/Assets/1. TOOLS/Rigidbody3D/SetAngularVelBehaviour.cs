using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SetAngularVelBehaviour : MonoBehaviour
{
    public enum Directions { X, Y, Z }
    public enum RotationTypes { Global, Local}
    public enum Modes { OnStart, OnCallOnly, ConstantRotation }
    
    public float amount;
    public Directions alongAxis = Directions.X;
    public RotationTypes rotationType = RotationTypes.Global;
    public Modes mode = Modes.ConstantRotation;
    
    private Rigidbody _myRigidbody;
    private Vector3 _actualDirection;
    private Vector3 _axisDirection;
    
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
        
        switch (alongAxis)
        {
            case Directions.X:
                _axisDirection = Vector3.right;
                break;
            case Directions.Y:
                _axisDirection = Vector3.up;
                break;
            case Directions.Z:
                _axisDirection = Vector3.forward;
                break;
            default:
                _axisDirection = Vector3.right;
                break;
        }

        if (rotationType == RotationTypes.Global)
        {
            _actualDirection = _axisDirection;
        }
        else if (rotationType == RotationTypes.Local)
        {
            _actualDirection = transform.TransformDirection(_axisDirection);
        }

        if (mode == Modes.OnStart)
        {
            SetAngularVelocity();
        }
        else if (mode == Modes.ConstantRotation)
        {
            StartCoroutine(SetConstantRotation());
        }
    }

    public void SetRotationAmount(float inputamount)
    {
        amount = inputamount;
    }

    public void SetAngularVelocity()
    {
        if (rotationType == RotationTypes.Local)
        {
            _actualDirection = transform.TransformDirection(_axisDirection);
        }
        _myRigidbody.angularVelocity = _actualDirection * amount;
    }

    IEnumerator SetConstantRotation()
    {
        while (true)
        {
            if (rotationType == RotationTypes.Local)
            {
                _actualDirection = transform.TransformDirection(_axisDirection);
            }
            _myRigidbody.angularVelocity = _actualDirection * amount;
            yield return 0;
        }
    }
}
