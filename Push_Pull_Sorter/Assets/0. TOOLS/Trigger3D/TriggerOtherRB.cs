using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class TriggerOtherRB : MonoBehaviour
{
    public enum Modes { DisableOtherRigidbody, EnableOtherRigidbody }

    public Modes mode = Modes.DisableOtherRigidbody;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            if (mode == Modes.DisableOtherRigidbody)
            {
                other.GetComponent<Rigidbody>().isKinematic = true;
            }
            else
            {
                other.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}