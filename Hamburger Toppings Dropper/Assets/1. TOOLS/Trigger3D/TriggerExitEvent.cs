using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class TriggerExitEvent : MonoBehaviour
{
    public UnityEvent<Transform> triggerExitEvent;
    
    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }
    
    private void OnTriggerExit(Collider other)
    {
        triggerExitEvent.Invoke(other.transform);
    }
}
