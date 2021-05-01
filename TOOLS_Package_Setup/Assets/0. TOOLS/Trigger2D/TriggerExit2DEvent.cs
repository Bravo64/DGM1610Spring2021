using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class TriggerExit2DEvent : MonoBehaviour
{
    public UnityEvent triggerExit2DEvent;
    
    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        triggerExit2DEvent.Invoke();
    }
}
