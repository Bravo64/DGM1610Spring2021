using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class TagSnapTriggerToMe : MonoBehaviour
{
    public string tagName;
    public bool snapTheirPosition = true;
    public bool snapTheirRotation = true;
    public bool snapX = true;
    public bool snapY = true;
    public bool snapZ = true;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(tagName)) { return; }
        
        Vector3 originalPosition = other.transform.position;
        
        if (snapTheirPosition)
        {
            other.transform.position = transform.position;
        }
        
        if (snapTheirRotation)
        {
            other.transform.rotation  = transform.rotation;
        }

        Vector3 currentPosition = other.transform.position;
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
        other.transform.position = currentPosition;
    }
}
