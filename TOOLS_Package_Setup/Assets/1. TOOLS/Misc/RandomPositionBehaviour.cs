using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomPositionBehaviour : MonoBehaviour
{
    [SerializeField]
    private float minPosition = -10.0f;
    [SerializeField]
    private float maxPosition = 10.0f;
    [SerializeField] 
    private bool runOnStart = true;
    private enum Axes { X, Y, Z}
    [SerializeField] 
    private Axes alongAxis = Axes.X;
    private enum Modes { RelativeToZeroPos, RelativeToCurrentPos}
    [SerializeField] 
    private Modes mode = Modes.RelativeToZeroPos;

    private Vector3 _currentPos;
    
    void Start()
    {
        if (runOnStart)
        {
            if (mode == Modes.RelativeToZeroPos)
            {
                RandomPosFromZero();
            }
            else
            {
                RandomPosFromCurrent();
            }
        }
    }

    public void RandomPosFromZero()
    {
        _currentPos = transform.position;
        float randomValue = Random.Range(minPosition, maxPosition);

        switch (alongAxis)
        {
            case Axes.X:
                _currentPos.x = randomValue;
                break;
            case Axes.Y:
                _currentPos.y = randomValue;
                break;
            case Axes.Z:
                _currentPos.z = randomValue;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        transform.position = _currentPos;
    }
    
    public void RandomPosFromCurrent()
    {
        _currentPos = transform.position;
        float randomValue = Random.Range(minPosition, maxPosition);

        switch (alongAxis)
        {
            case Axes.X:
                _currentPos.x += randomValue;
                break;
            case Axes.Y:
                _currentPos.y += randomValue;
                break;
            case Axes.Z:
                _currentPos.z += randomValue;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        transform.position = _currentPos;
    }
}
