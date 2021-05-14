using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class TagTriggerDisableOther : MonoBehaviour
{
    public string tagName;
    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(tagName)) { return; }
        
        other.gameObject.SetActive(false);
    }
}
