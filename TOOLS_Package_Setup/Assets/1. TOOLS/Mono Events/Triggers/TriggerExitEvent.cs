using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerExitEvent : MonoBehaviour
{
    public UnityEvent triggerExitEvent;
    
    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }
    
    private void OnTriggerExit(Collider other)
    {
        triggerExitEvent.Invoke();
    }
}
