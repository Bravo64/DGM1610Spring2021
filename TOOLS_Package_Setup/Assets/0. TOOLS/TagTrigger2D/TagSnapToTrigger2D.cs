using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class TagSnapToTrigger2D : MonoBehaviour
{
    public enum Modes { Enabled, Disabled}

    public string tagName;
    public bool snapToOtherPosition = true;
    public bool snapToOtherRotation = true;
    public Modes applySnapToParent = Modes.Disabled;
    private Transform _currentTransform;
    public bool ApplyToX = true;
    public bool ApplyToY = true;
    
    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
        
        if (applySnapToParent == Modes.Enabled)
        {
            _currentTransform = transform.parent;
        }
        else
        {
            _currentTransform = transform;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(tagName)) { return; }
        
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
        if (!ApplyToX)
        {
            currentPosition.x = originalPosition.x;
        }

        if (!ApplyToY)
        {
            currentPosition.y = originalPosition.y;
        }
        _currentTransform.position = currentPosition;
    }
}
