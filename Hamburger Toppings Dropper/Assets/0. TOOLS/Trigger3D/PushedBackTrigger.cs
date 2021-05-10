using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class PushedBackTrigger : MonoBehaviour
{
    public float pushForce;
    public Vector3 additionalGlobalForce = Vector3.up;
    public bool triggerPushesParent = true;
    
    private Rigidbody _activeRigidbody;
    private Vector3 _direction;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;

        if (triggerPushesParent)
        {
            _activeRigidbody = transform.parent.GetComponent<Rigidbody>();
            GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            _activeRigidbody = transform.GetComponent<Rigidbody>();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (_activeRigidbody)
        {
            _direction = (transform.position - other.transform.position).normalized;
            _activeRigidbody.AddForce(_direction * pushForce + additionalGlobalForce);
        }
    }
}