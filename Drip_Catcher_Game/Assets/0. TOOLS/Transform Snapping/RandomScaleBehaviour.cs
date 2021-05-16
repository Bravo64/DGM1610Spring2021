using UnityEngine;
using Random = UnityEngine.Random;

public class RandomScaleBehaviour : MonoBehaviour
{
    public enum Axes { X, Y, Z }

    public float minScaleLimit, maxScaleLimit;
    public bool runOnStart = true;
    public Axes applyToAxis = Axes.X;

    private Vector3 _currentScale;
    
    void Start()
    {
        if (runOnStart)
        {
            ApplyRandomScale();
        }
    }

    public void ApplyRandomScale()
    {
        _currentScale = transform.localScale;
        float randomValue = Random.Range(minScaleLimit, maxScaleLimit);
        switch (applyToAxis)
        {
            case Axes.X:
                _currentScale.x = randomValue;
                break;
            case Axes.Y:
                _currentScale.y = randomValue;
                break;
            case Axes.Z:
                _currentScale.z = randomValue;
                break;
        }
        transform.localScale = _currentScale;

    }
}
