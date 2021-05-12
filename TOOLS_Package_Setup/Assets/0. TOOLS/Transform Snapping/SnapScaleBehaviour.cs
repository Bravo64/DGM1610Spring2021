using UnityEngine;

public class SnapScaleBehaviour : MonoBehaviour
{
    public enum Modes { SnapToVector3Reference, SnapToTransformReference, SnapToVector3DataReference }
    
    public Modes mode = Modes.SnapToVector3Reference;
    public Vector3 vector3Reference;
    public Transform transformReference;
    public Vector3Data vector3DataReference;
    public bool applyToParent = false;
    public bool runOnStart = true;

    private Transform _currentTransform;
    
    void Start()
    {
        if (applyToParent)
        {
            _currentTransform = transformReference.parent;
        }
        else
        {
            _currentTransform = transform;
        }
        
        if (runOnStart)
        {
            ApplyRotationSnapping();
        }
    }

    void ApplyRotationSnapping()
    {
        switch (mode)
        {
            case Modes.SnapToVector3Reference:
                _currentTransform.localScale = vector3Reference;
                break;
            case Modes.SnapToTransformReference:
                _currentTransform.localScale = transformReference.localScale;
                break;
            case Modes.SnapToVector3DataReference:
                _currentTransform.localScale = vector3DataReference.value;
                break;
            default:
                _currentTransform.localScale = vector3Reference;
                break;
        }
    }
}
