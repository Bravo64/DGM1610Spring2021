using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class AdoptChildTrigger2D : MonoBehaviour
{
    public bool childToMyParent = false;
    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (childToMyParent)
        {
            other.transform.parent = transform.parent;
        }
        else
        {
            other.transform.parent = transform;
        }
    }
}
