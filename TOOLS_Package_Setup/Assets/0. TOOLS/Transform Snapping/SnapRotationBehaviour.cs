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
        _savedRotation.x = ProcessOneAxisSnap(vector3Reference.x, transformReference.eulerAngles.x, vector3DataReference.value.x);
        objectToSnap.eulerAngles = _savedRotation;
    }
    
    public void ApplyYOnlyRotationSnapping()
    {
        _savedRotation = objectToSnap.eulerAngles;
        _savedRotation.y = ProcessOneAxisSnap(vector3Reference.y, transformReference.eulerAngles.y, vector3DataReference.value.y);
        objectToSnap.eulerAngles = _savedRotation;
    }
    
    public void ApplyZOnlyRotationSnapping()
    {
        _savedRotation = objectToSnap.eulerAngles;
        _savedRotation.z = ProcessOneAxisSnap(vector3Reference.z, transformReference.eulerAngles.z, vector3DataReference.value.z);
        objectToSnap.eulerAngles = _savedRotation;
    }
    
    private float ProcessOneAxisSnap(float vectorAxis, float transformAxis, float vectorDataAxis)
    {
        switch (mode)
        {
            case Modes.SnapToVector3Reference:
                return vectorAxis;
            case Modes.SnapToTransformReference:
                return transformAxis;
            case Modes.SnapToVector3DataReference:
                return vectorDataAxis;
            default:
                return vectorAxis;
        }
    }
}
