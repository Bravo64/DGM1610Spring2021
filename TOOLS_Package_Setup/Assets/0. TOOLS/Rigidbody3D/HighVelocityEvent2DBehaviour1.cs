using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class HighVelocityEventBehaviour : MonoBehaviour
{
    public float maxVelocity;
    public UnityEvent highVelocityEvent;
    
    private Rigidbody _myRigidbody;

    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (_myRigidbody.velocity.magnitude > maxVelocity)
        {
            highVelocityEvent.Invoke();
        }
    }
}
