using UnityEngine;
using Random = UnityEngine.Random;

public class RandomRotationBehaviour : MonoBehaviour
{
    public enum Axes { X, Y, Z}

    public enum RotationTypes {Local, Global}

    public bool limitRandomRotation = false;
    public float minRotationLimit, maxRotationLimit;
    public bool runOnStart = true;
    public Axes aroundAxis = Axes.X;
    public RotationTypes rotationType = RotationTypes.Local;
    

    private Vector3 _currentRotation;
    
    void Start()
    {
        if (runOnStart)
        {
            ApplyRandomRotation();
        }
    }

    public void ApplyRandomRotation()
    {
        if (rotationType == RotationTypes.Local)
        {
            _currentRotation = transform.localEulerAngles;
        }
        else
        {
            _currentRotation = transform.eulerAngles;
        }
        
        float randomValue;
        if (limitRandomRotation)
        {
            randomValue = Random.Range(minRotationLimit, maxRotationLimit);
        }
        else
        {
            randomValue = Random.Range(0.0f, 360.0f);
        }

        switch (aroundAxis)
        {
            case Axes.X:
                _currentRotation.x = randomValue;
                break;
            case Axes.Y:
                _currentRotation.y = randomValue;
                break;
            case Axes.Z:
                _currentRotation.z = randomValue;
                break;
            default:
                _currentRotation.x = randomValue;
                break;
        }

        if (rotationType == RotationTypes.Local)
        {
            transform.localRotation = Quaternion.Euler(_currentRotation);
        }
        else
        {
            transform.rotation = Quaternion.Euler(_currentRotation);
        }
    }
}
