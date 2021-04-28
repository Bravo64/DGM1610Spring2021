using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SetAngularVel2DBehaviour : MonoBehaviour
{
    public enum Modes { OnStart, OnCallOnly, ConstantRotation }
    
    public float amount;
    public Modes mode = Modes.ConstantRotation;
    private Rigidbody2D _myRigidbody2D;

    void Start()
    {
        _myRigidbody2D = GetComponent<Rigidbody2D>();

        if (mode == Modes.OnStart)
        {
            SetAngularVelocity();
        }
        else if (mode == Modes.ConstantRotation)
        {
            StartCoroutine(SetConstantRotation());
        }
    }

    public void SetForceAmount(float inputAmount)
    {
        amount = inputAmount;
    }

    public void SetAngularVelocity()
    {
        _myRigidbody2D.angularVelocity = amount;
    }

    IEnumerator SetConstantRotation()
    {
        while (true)
        {
            _myRigidbody2D.angularVelocity = amount;
            yield return 0;
        }
    }
}
