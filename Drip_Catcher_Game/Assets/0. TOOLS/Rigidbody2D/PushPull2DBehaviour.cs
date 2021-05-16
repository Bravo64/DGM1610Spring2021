using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PushPull2DBehaviour : MonoBehaviour
{
    public enum ForceType { PullTowardsTarget, PushAwayFromTarget }
    public enum Modes { ConstantForce, ForceOneFrame }
    
    public float forceAmount;
    public Transform targetObject;
    public ForceType forceType = ForceType.PullTowardsTarget;
    public Modes mode = Modes.ConstantForce;
    public bool beginOnStart = true;

    private Rigidbody2D _myRigidbody2D;
    private Vector2 _direction;
    private float _forceActual;

    void Start()
    {
        _myRigidbody2D = GetComponent<Rigidbody2D>();

        if (forceType == ForceType.PullTowardsTarget)
        {
            _forceActual = forceAmount;
        }
        else
        {
            _forceActual = -forceAmount;
        }
        
        if (beginOnStart)
        {
            if (mode == Modes.ConstantForce)
            {
                ActivateConstantForce();
            }
            else
            {
                ApplyForceOneFrame();
            }
        }
    }
    
    public void ApplyForceOneFrame()
    {
        _direction = (targetObject.transform.position - transform.position).normalized;
        _myRigidbody2D.AddForce(_direction * _forceActual);
    }
    
    public void ActivateConstantForce()
    {
        StartCoroutine(ConstantForce());
    }

    IEnumerator ConstantForce()
    {
        while (true)
        {
            ApplyForceOneFrame();
            yield return 0;
        }
    }
    
    public void PauseConstantForce()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        PauseConstantForce();
    }
}
