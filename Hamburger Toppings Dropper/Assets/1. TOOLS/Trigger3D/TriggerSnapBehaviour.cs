using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class TriggerSnapBehaviour : MonoBehaviour
{
    public enum Modes { Enabled, Disabled}

    public bool snapToOtherPosition = true;
    public bool snapToOtherRotation = true;
    public Modes applySnapToParent = Modes.Disabled;
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
        if (snapToOtherPosition)
        {
            _currentTransform.position = other.transform.position;
        }
        
        if (snapToOtherRotation)
        {
            _currentTransform.rotation = other.transform.rotation;
        }
    }
}
