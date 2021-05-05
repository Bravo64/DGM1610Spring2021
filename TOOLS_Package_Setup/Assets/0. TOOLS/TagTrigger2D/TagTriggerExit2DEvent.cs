using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class TagTriggerExit2DEvent : MonoBehaviour
{
    public string tagName;
    public UnityEvent triggerExit2DEvent;
    
    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag(tagName)) { return; }
        
        triggerExit2DEvent.Invoke();
    }
}
