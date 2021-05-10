using System.Collections.Generic;
using UnityEngine;

public class DynamicInstancePoolBehavior : MonoBehaviour
{
    public enum Modes { UseObjectPoolList, UseChildrenAsObjectPool }
    public enum RotationTypes { CopyPointRotation, QuaternionIdentity }
    
    public Transform creationPoint;
    public Modes mode = Modes.UseObjectPoolList;
    public List<GameObject> objectPoolList = new List<GameObject>();
    public GameObject[] backupPrefabs;
    public RotationTypes rotationType = RotationTypes.CopyPointRotation;
    public bool randomizeOrder = false;
    public bool beginOnStart = false;

    private int _randNum;

    private void Start()
    {
        if (mode == Modes.UseChildrenAsObjectPool)
        {
            objectPoolList.Clear();
            foreach (Transform child in transform)
            {
                objectPoolList.Add(child.gameObject);
            }
        }
        
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
        for (int i = 0; i < objectPoolList.Count; i++)
        {
            if (!objectPoolList[i].activeSelf)
            {
                ApplyActivation(objectPoolList[i]);
                return;
            }
        }
        InstantiateBackup(backupPrefabs[Random.Range(0, backupPrefabs.Length)]);
    }
    
    public void ActivateRandomPoolInstance()
    {
        
        for (int i = 0; i < 100; i++)
        {
            _randNum = Random.Range(0, objectPoolList.Count);
            if (!objectPoolList[_randNum].activeSelf)
            {
                ApplyActivation(objectPoolList[_randNum]);
                return;
            }
        }
        InstantiateBackup(backupPrefabs[Random.Range(0, backupPrefabs.Length)]);
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

    private void InstantiateBackup(GameObject inputObject)
    {
        GameObject backupObject;
        if (rotationType == RotationTypes.CopyPointRotation)
        {
            backupObject = (GameObject)Instantiate(inputObject, creationPoint.transform.position, creationPoint.transform.rotation);
        }
        else
        {
            backupObject = (GameObject)Instantiate(inputObject, creationPoint.transform.position, Quaternion.identity);
        }
        objectPoolList.Add(backupObject);
        
        if (mode == Modes.UseChildrenAsObjectPool)
        {
            backupObject.transform.parent = transform;
        }
    }
}