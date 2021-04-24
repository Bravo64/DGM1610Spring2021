using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class TriggerEvent : MonoBehaviour
{
    public UnityEvent triggerEnterEvent;
    
    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        triggerEnterEvent.Invoke();
    }
}
