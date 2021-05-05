using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class TagTriggerOtherRB : MonoBehaviour
{
    public enum Modes { DisableOtherRigidbody, EnableOtherRigidbody }

    public string tagName;
    public Modes mode = Modes.DisableOtherRigidbody;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(tagName)) { return; }
        
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