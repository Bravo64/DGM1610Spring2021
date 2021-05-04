using UnityEngine;

public class SnapRotationBehaviour : MonoBehaviour
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
                transform.rotation = Quaternion.Euler(vector3Reference);
                break;
            case Modes.SnapToTransformReference:
                transform.rotation = transformReference.rotation;
                break;
            case Modes.SnapToVector3DataReference:
                transform.rotation = Quaternion.Euler(vector3DataReference.value);
                break;
            default:
                transform.rotation = Quaternion.Euler(vector3Reference);
                break;
        }
    }
}