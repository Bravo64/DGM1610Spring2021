using UnityEngine;

public class SnapPositionBehaviour : MonoBehaviour
{
    public enum Modes { SnapToVector3Reference, SnapToTransformReference, SnapToVector3DataReference }
    
    public Modes mode = Modes.SnapToVector3Reference;
    public bool vectorIsRelative = false;
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
            ApplyPositionSnapping();
        }
    }

    public void ApplyPositionSnapping()
    {
        switch (mode)
        {
            case Modes.SnapToVector3Reference:
                if (vectorIsRelative)
                {
                    _currentTransform.position += vector3Reference;
                }
                else
                {
                    _currentTransform.position = vector3Reference;
                }
                break;
            case Modes.SnapToTransformReference:
                _currentTransform.position = transformReference.position;
                break;
            case Modes.SnapToVector3DataReference:
                _currentTransform.position = vector3DataReference.value;
                break;
            default:
                _currentTransform.position = vector3Reference;
                break;
        }
    }
}