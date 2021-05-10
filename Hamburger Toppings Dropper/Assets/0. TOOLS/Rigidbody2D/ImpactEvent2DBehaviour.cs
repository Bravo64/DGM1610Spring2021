using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class ImpactEvent2DBehaviour : MonoBehaviour
{
    public float velocityThreshold = 1.0f;
    public UnityEvent onImpactEvent;
    
    private Rigidbody2D _myRigidbody2D;
    private float _savedVelocity;
    void Start()
    {
        _myRigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (_myRigidbody2D.velocity.magnitude < _savedVelocity - velocityThreshold)
        {
            onImpactEvent.Invoke();
        }
        _savedVelocity = _myRigidbody2D.velocity.magnitude;
    }
}
