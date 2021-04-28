using System;
using UnityEngine;

public class InstanciationBehavior : MonoBehaviour
{
    public enum RotationTypes { CopyPointRotation, QuaternionIdentity }
    
    public GameObject objectToCreate, creationPoint;
    public RotationTypes rotationType = RotationTypes.CopyPointRotation;
    public bool instanciateOnStart = false;

    private void Start()
    {
        if (instanciateOnStart)
        {
            ActivateInstanciation();
        }
    }

    public void ActivateInstanciation()
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
