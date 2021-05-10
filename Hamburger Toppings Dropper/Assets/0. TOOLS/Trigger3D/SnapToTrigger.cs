using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class SnapToTrigger : MonoBehaviour
{
    public enum Modes { Enabled, Disabled}

    public bool snapToOtherPosition = true;
    public bool snapToOtherRotation = true;
    public Modes applySnapToParent = Modes.Disabled;
    public bool snapX = true;
    public bool snapY = true;
    public bool snapZ = true;
    
    private Transform _currentTransform;
    
    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;
        
        if (applySnapToParent == Modes.Enabled)
        {
            _currentTransform = transform.parent;
        }
        else
        {
            _currentTransform = transform;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Vector3 originalPosition = _currentTransform.position;
        
        if (snapToOtherPosition)
        {
            _currentTransform.position = other.transform.position;
        }
        
        if (snapToOtherRotation)
        {
            _currentTransform.rotation = other.transform.rotation;
        }

        Vector3 currentPosition = _currentTransform.position;
        if (!snapX)
        {
            currentPosition.x = originalPosition.x;
        }

        if (!snapY)
        {
            currentPosition.y = originalPosition.y;
        }
        
        if (!snapZ)
        {
            currentPosition.z = originalPosition.z;
        }
        _currentTransform.position = currentPosition;
    }
}
