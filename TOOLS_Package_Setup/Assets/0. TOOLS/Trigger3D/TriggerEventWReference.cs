using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class TriggerEventWReference : MonoBehaviour
{
    public UnityEvent<GameObject> triggerEnterEvent;
    public GameObject otherObject;
    
    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        otherObject = other.gameObject;
        triggerEnterEvent.Invoke(otherObject);
    }
}
