using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PushPullBehaviour : MonoBehaviour
{
    public enum ForceType { PullTowardsTarget, PushAwayFromTarget }
    public enum Modes { ConstantForce, ForceOneFrame }
    
    public float forceAmount;
    public Transform targetObject;
    public ForceType forceType = ForceType.PullTowardsTarget;
    public Modes mode = Modes.ConstantForce;
    public bool beginOnStart = true;

    private Rigidbody _myRigidbody;
    private Vector3 _direction;
    private float _forceActual;

    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();

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

    public void TogglePushPull()
    {
        _forceActual = -_forceActual;

        if (forceType == ForceType.PullTowardsTarget)
        {
            forceType = ForceType.PushAwayFromTarget;
        }
        else
        {
            forceType = ForceType.PullTowardsTarget;
        }
    }
    
    public void ApplyForceOneFrame()
    {
        _direction = (targetObject.transform.position - transform.position).normalized;
        _myRigidbody.AddForce(_direction * _forceActual);
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
