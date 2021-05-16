using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class HighVelocityEvent2DBehaviour : MonoBehaviour
{
    public float maxVelocity;
    public UnityEvent highVelocityEvent;
    
    private Rigidbody2D _myRigidbody2D;

    void Start()
    {
        _myRigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (_myRigidbody2D.velocity.magnitude > maxVelocity)
        {
            highVelocityEvent.Invoke();
        }
    }
}
