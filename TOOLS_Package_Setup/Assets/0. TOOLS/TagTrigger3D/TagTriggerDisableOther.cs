using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class TagTriggerDisableOther : MonoBehaviour
{
    public string tagName;
    public bool disableOtherParent = false;
    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(tagName)) { return; }
        
        if (disableOtherParent)
        {
            other.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            other.gameObject.SetActive(false);
        }
    }
}
