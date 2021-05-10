using UnityEngine;

public class InstantiationBehavior : MonoBehaviour
{
    public enum RotationTypes { CopyPointRotation, QuaternionIdentity }
    
    public GameObject objectToCreate, creationPoint;
    public RotationTypes rotationType = RotationTypes.CopyPointRotation;
    public bool instantiateOnStart = false;

    private void Start()
    {
        if (instantiateOnStart)
        {
            ActivateInstantiation();
        }
    }

    public void SetObjectToCreate(GameObject inputObject)
    {
        objectToCreate = inputObject;
    }

    public void ActivateInstantiation()
    {
        if (rotationType == RotationTypes.CopyPointRotation)
        {
            Instantiate(objectToCreate, creationPoint.transform.position, creationPoint.transform.rotation);
        }
        else
        {
            Instantiate(objectToCreate, creationPoint.transform.position, Quaternion.identity);
        }
    }
}