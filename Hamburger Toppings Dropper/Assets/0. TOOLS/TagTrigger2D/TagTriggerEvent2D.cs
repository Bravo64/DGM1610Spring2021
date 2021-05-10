using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class TagTriggerEvent2D : MonoBehaviour
{
    public string tagName;
    public UnityEvent triggerEnterEvent;
    
    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(tagName)) { return; }
        
        triggerEnterEvent.Invoke();
    }
}
