using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class TriggerTransformEvent2D : MonoBehaviour
{
    public UnityEvent<Transform> triggerEnterEvent;

    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        triggerEnterEvent.Invoke(other.transform);
    }
}
