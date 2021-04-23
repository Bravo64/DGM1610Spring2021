using System;
using UnityEngine;

public class InstanciationBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToCreate;
    [SerializeField] 
    private Transform creationPoint;
    private enum RotationTypes { CopyPointRotation, QuaternionIdentity }
    [SerializeField] 
    private RotationTypes rotationType = RotationTypes.CopyPointRotation;
    [SerializeField]
    private bool instanciateOnStart = false;

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
            Instantiate(objectToCreate, creationPoint.position, creationPoint.rotation);
        }
        else
        {
            Instantiate(objectToCreate, creationPoint.position, Quaternion.identity);
        }
    }
}
