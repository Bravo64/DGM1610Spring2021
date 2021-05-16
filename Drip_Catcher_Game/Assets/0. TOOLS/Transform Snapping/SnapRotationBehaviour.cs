using UnityEngine;

public class SnapRotationBehaviour : MonoBehaviour
{
    public enum Modes { SnapToVector3Reference, SnapToTransformReference, SnapToVector3DataReference }
    public enum SnapAxes { SnapAllAxes, SnapXOnly, SnapYOnly, SnapZOnly }
    
    public Transform objectToSnap;
    public Modes mode = Modes.SnapToVector3Reference;
    public Vector3 vector3Reference;
    public Transform transformReference;
    public Vector3Data vector3DataReference;
    public SnapAxes snapAxesType = SnapAxes.SnapAllAxes;
    public bool runOnStart = true;

    private Vector3 _savedRotation;
    
    void Start()
    {
        if (runOnStart)
        {
            switch (snapAxesType)
            {
                case SnapAxes.SnapAllAxes:
                    ApplyFullRotationSnapping();
                    break;
                case SnapAxes.SnapXOnly:
                    ApplyXOnlyRotationSnapping();
                    break;
                case SnapAxes.SnapYOnly:
                    ApplyYOnlyRotationSnapping();
                    break;
                case SnapAxes.SnapZOnly:
                    ApplyZOnlyRotationSnapping();
                    break;
            }
        }
    }

    public void ApplyFullRotationSnapping()
    {
        switch (mode)
        {
            case Modes.SnapToVector3Reference:
                objectToSnap.rotation = Quaternion.Euler(vector3Reference);
                break;
            case Modes.SnapToTransformReference:
                objectToSnap.rotation = transformReference.rotation;
                break;
            case Modes.SnapToVector3DataReference:
                objectToSnap.rotation = Quaternion.Euler(vector3DataReference.value);
                break;
            default:
                objectToSnap.rotation = Quaternion.Euler(vector3Reference);
                break;
        }
    }
    
    public void ApplyXOnlyRotationSnapping()
    {
        _savedRotation = objectToSnap.eulerAngles;
        switch (mode)
        {
            case Modes.SnapToVector3Reference:
                _savedRotation.x = vector3Reference.x;
                break;
            case Modes.SnapToTransformReference:
                _savedRotation.x = transformReference.eulerAngles.x;
                break;
            case Modes.SnapToVector3DataReference:
                _savedRotation.x = vector3DataReference.value.x;
                break;
        }
        objectToSnap.eulerAngles = _savedRotation;
    }
    
    public void ApplyYOnlyRotationSnapping()
    {
        _savedRotation = objectToSnap.eulerAngles;
        switch (mode)
        {
            case Modes.SnapToVector3Reference:
                _savedRotation.y = vector3Reference.y;
                break;
            case Modes.SnapToTransformReference:
                _savedRotation.y = transformReference.eulerAngles.y;
                break;
            case Modes.SnapToVector3DataReference:
                _savedRotation.y = vector3DataReference.value.y;
                break;
        }
        objectToSnap.eulerAngles = _savedRotation;
    }
    
    public void ApplyZOnlyRotationSnapping()
    {
        _savedRotation = objectToSnap.eulerAngles;
        switch (mode)
        {
            case Modes.SnapToVector3Reference:
                _savedRotation.z = vector3Reference.z;
                break;
            case Modes.SnapToTransformReference:
                _savedRotation.z = transformReference.eulerAngles.z;
                break;
            case Modes.SnapToVector3DataReference:
                _savedRotation.z = vector3DataReference.value.z;
                break;
        }
        objectToSnap.eulerAngles = _savedRotation;
    }
}
