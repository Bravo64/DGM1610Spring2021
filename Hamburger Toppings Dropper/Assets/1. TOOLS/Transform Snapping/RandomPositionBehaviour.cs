using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomPositionBehaviour : MonoBehaviour
{
    public enum Axes { X, Y, Z}
    public enum Modes { RelativeToZeroPos, RelativeToCurrentPos}
    
    public float minPosition, maxPosition;
    public bool runOnStart = true;
    public Axes alongAxis = Axes.X;
    public Modes mode = Modes.RelativeToZeroPos;

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
