using UnityEngine;

public class InstancePoolBehavior : MonoBehaviour
{
    public enum RotationTypes { CopyPointRotation, QuaternionIdentity }
    
    public Transform creationPoint;
    public GameObject[] objectPool;
    public RotationTypes rotationType = RotationTypes.CopyPointRotation;
    public bool randomizeOrder = false;
    public bool beginOnStart = false;

    private int _randNum;

    private void Start()
    {
        if (beginOnStart)
        {
            if (!randomizeOrder)
            {
                ActivatePoolInstance();
            }
            else
            {
                ActivateRandomPoolInstance();
            }
        }
    }

    public void ActivatePoolInstance()
    {
        for (int i = 0; i < objectPool.Length; i++)
        {
            if (!objectPool[i].activeSelf)
            {
                ApplyActivation(objectPool[i]);
                break;
            }
        }
    }
    
    public void ActivateRandomPoolInstance()
    {
        
        for (int i = 0; i < 100; i++)
        {
            _randNum = Random.Range(0, objectPool.Length);
            if (!objectPool[_randNum].activeSelf)
            {
                ApplyActivation(objectPool[_randNum]);
                break;
            }
        }
    }

    private void ApplyActivation(GameObject inputObject)
    {
        inputObject.transform.position = creationPoint.position;
        inputObject.SetActive(true);
        
        if (rotationType == RotationTypes.CopyPointRotation)
        {
            inputObject.transform.rotation = creationPoint.rotation;
        }
        else
        {
            inputObject.transform.rotation = Quaternion.identity;
        }
    }
}