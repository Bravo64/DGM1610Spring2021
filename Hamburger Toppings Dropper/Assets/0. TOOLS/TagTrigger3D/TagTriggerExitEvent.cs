using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class TagTriggerExitEvent : MonoBehaviour
{
    public string tagName;
    public UnityEvent<Transform> triggerExitEvent;
    
    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(tagName)) { return; }
        
        triggerExitEvent.Invoke(other.transform);
    }
}
