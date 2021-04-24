using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Trigger2DEvent : MonoBehaviour
{
    public UnityEvent triggerEnter2DEvent;

    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        triggerEnter2DEvent.Invoke();
    }
}
