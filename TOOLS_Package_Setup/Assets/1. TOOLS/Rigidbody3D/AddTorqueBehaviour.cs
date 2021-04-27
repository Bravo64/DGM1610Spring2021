using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AddTorqueBehaviour : MonoBehaviour
{
    [SerializeField] 
    private float amount;
    private enum Axes { X, Y, Z }
    private enum RotationTypes { Global, Local}
    private enum Modes { OnStart, OnCallOnly, ConstantRotation }
    [SerializeField]
    private Axes aroundAxis = Axes.X;
    [SerializeField]
    private RotationTypes rotationType = RotationTypes.Global;
    [SerializeField]
    private Modes mode = Modes.ConstantRotation;

    private Rigidbody _myRigidbody;
    private Vector3 _axisDirection;
    
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();

        switch (aroundAxis)
        {
            case Axes.X:
                _axisDirection = Vector3.right;
                break;
            case Axes.Y:
                _axisDirection = Vector3.up;
                break;
            case Axes.Z:
                _axisDirection = Vector3.forward;
                break;
            default:
                _axisDirection = Vector3.right;
                break;
        }

        if (mode == Modes.OnStart)
        {
            ApplyTorque();
        }
        else if (mode == Modes.ConstantRotation)
        {
            if (rotationType == RotationTypes.Global)
            {
                StartCoroutine(ApplyConstantGlobalRotation());
            }
            else
            {
                StartCoroutine(ApplyConstantLocalRotation());
            }
        }
    }

    public void SetForceAmount(float inputAmount)
    {
        amount = inputAmount;
    }

    public void ApplyTorque()
    {
        if (rotationType == RotationTypes.Global)
        {
            _myRigidbody.AddTorque(amount * _axisDirection);
        }
        else
        {
            _myRigidbody.AddRelativeTorque(amount * _axisDirection);
        }
    }

    IEnumerator ApplyConstantLocalRotation()
    {
        while (true)
        {
            _myRigidbody.AddRelativeTorque(amount * _axisDirection);
            yield return 0;
        }
    }
    
    IEnumerator ApplyConstantGlobalRotation()
    {
        while (true)
        {
            _myRigidbody.AddTorque(amount * _axisDirection);
            yield return 0;
        }
    }
}
