using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class TriggerTagEvent : MonoBehaviour
{
    public string tagName;
    public UnityEvent triggerEnterEvent;
    
    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagName))
        {
            triggerEnterEvent.Invoke();
        }
    }
}
