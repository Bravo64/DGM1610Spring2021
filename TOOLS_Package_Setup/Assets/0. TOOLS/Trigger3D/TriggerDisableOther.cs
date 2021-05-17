using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class TriggerDisableOther : MonoBehaviour
{
    public bool disableOtherParent = false;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
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
