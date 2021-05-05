using UnityEngine;

public class InstantiateRandomBehavior : MonoBehaviour
{
    public enum RotationTypes { CopyPointRotation, QuaternionIdentity }

    public GameObject[] randomPrefabObjects;
    public Transform creationPoint;
    public RotationTypes rotationType = RotationTypes.CopyPointRotation;
    public bool instantiateOnStart = false;

    private int randomIndex;

    private void Start()
    {
        if (instantiateOnStart)
        {
            ActivateRandomInstantiation();
        }
    }

    public void ActivateRandomInstantiation()
    {
        randomIndex = Random.Range(0, randomPrefabObjects.Length);
        if (rotationType == RotationTypes.CopyPointRotation)
        {
            Instantiate(randomPrefabObjects[randomIndex], creationPoint.position, creationPoint.rotation);
        }
        else
        {
            Instantiate(randomPrefabObjects[randomIndex], creationPoint.position, Quaternion.identity);
        }
    }
}