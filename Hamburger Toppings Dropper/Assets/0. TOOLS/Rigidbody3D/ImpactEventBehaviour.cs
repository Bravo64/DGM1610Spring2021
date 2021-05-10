using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class ImpactEventBehaviour : MonoBehaviour
{
    public float velocityThreshold = 1.0f;
    public UnityEvent onImpactEvent;
    
    private Rigidbody _myRigidbody;
    private float _savedVelocity;
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (_myRigidbody.velocity.magnitude < _savedVelocity - velocityThreshold)
        {
            onImpactEvent.Invoke();
        }
        _savedVelocity = _myRigidbody.velocity.magnitude;
    }
}
