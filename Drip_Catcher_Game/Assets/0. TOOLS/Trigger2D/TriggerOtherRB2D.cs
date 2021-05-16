using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class TriggerOtherRB2D : MonoBehaviour
{
    public enum Modes { DisableOtherRigidbody2D, EnableOtherRigidbody2D }

    public Modes mode = Modes.DisableOtherRigidbody2D;

    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Rigidbody2D>())
        {
            if (mode == Modes.DisableOtherRigidbody2D)
            {
                other.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                other.gameObject.GetComponent<Rigidbody2D>().Sleep();
            }
            else
            {
                other.gameObject.GetComponent<Rigidbody2D>().WakeUp();
                other.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }
    }
}