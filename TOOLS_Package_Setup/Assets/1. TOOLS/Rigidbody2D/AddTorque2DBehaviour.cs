using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AddTorque2DBehaviour : MonoBehaviour
{
    [SerializeField] 
    private float amount;
    private enum Modes { OnStart, OnCallOnly, ConstantRotation }
    [SerializeField]
    private Modes mode = Modes.ConstantRotation;
    private Rigidbody2D _myRigidbody2D;

    void Start()
    {
        _myRigidbody2D = GetComponent<Rigidbody2D>();

        if (mode == Modes.OnStart)
        {
            ApplyTorque();
        }
        else if (mode == Modes.ConstantRotation)
        {
            StartCoroutine(ApplyConstantRotation());
        }
    }

    public void SetForceAmount(float inputAmount)
    {
        amount = inputAmount;
    }

    public void ApplyTorque()
    {
        _myRigidbody2D.AddTorque(amount);
    }

    IEnumerator ApplyConstantRotation()
    {
        while (true)
        {
            _myRigidbody2D.AddTorque(amount);
            yield return 0;
        }
    }
}
