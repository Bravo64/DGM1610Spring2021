using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class TagSnapTriggerToMe2D : MonoBehaviour
{
    public string tagName;
    public bool snapTheirPosition = true;
    public bool snapTheirRosition = true;
    public bool snapX = true;
    public bool snapY = true;

    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(tagName)) { return; }
        
        Vector2 originalPosition = other.transform.position;
        
        if (snapTheirPosition)
        {
            other.transform.position = transform.position;
        }
        
        if (snapTheirRosition)
        {
            other.transform.rotation  = transform.rotation;
        }

        Vector2 currentPosition = other.transform.position;
        if (!snapX)
        {
            currentPosition.x = originalPosition.x;
        }

        if (!snapY)
        {
            currentPosition.y = originalPosition.y;
        }
        other.transform.position = currentPosition;
    }
}