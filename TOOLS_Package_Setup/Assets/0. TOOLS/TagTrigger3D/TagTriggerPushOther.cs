using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class TagTriggerPushOther : MonoBehaviour
{
    public string tagName;
    public float pushForce;
    public Vector3 additionalGlobalForce = Vector3.up;
    public bool pushOtherParent = false;

    private Rigidbody _activeRigidbody;
    private Vector3 _direction;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(tagName)) { return; }
        
        if (pushOtherParent)
        {
            if (other.transform.parent.GetComponent<Rigidbody>())
            {
                _activeRigidbody  = other.transform.parent.GetComponent<Rigidbody>();
            }
        }
        else
        {
            if (other.GetComponent<Rigidbody>())
            {
                _activeRigidbody  = other.GetComponent<Rigidbody>();
            }
        }

        if (_activeRigidbody)
        {
            _direction = (other.transform.position - transform.position).normalized;
            _activeRigidbody.AddForce(_direction * pushForce + additionalGlobalForce);
        }
    }
}