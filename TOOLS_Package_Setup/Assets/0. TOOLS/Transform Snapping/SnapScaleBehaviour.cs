using UnityEngine;

public class SnapScaleBehaviour : MonoBehaviour
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

    private Vector3 _savedScale;
    
    void Start()
    {
        if (runOnStart)
        {
            switch (snapAxesType)
            {
                case SnapAxes.SnapAllAxes:
                    ApplyFullScaleSnapping();
                    break;
                case SnapAxes.SnapXOnly:
                    ApplyXOnlyScaleSnapping();
                    break;
                case SnapAxes.SnapYOnly:
                    ApplyYOnlyScaleSnapping();
                    break;
                case SnapAxes.SnapZOnly:
                    ApplyZOnlyScaleSnapping();
                    break;
            }
        }
    }

    public void ApplyFullScaleSnapping()
    {
        switch (mode)
        {
            case Modes.SnapToVector3Reference:
                objectToSnap.localScale = vector3Reference;
                break;
            case Modes.SnapToTransformReference:
                objectToSnap.localScale = transformReference.localScale;
                break;
            case Modes.SnapToVector3DataReference:
                objectToSnap.localScale = vector3DataReference.value;
                break;
            default:
                objectToSnap.localScale = vector3Reference;
                break;
        }
    }
    
    public void ApplyXOnlyScaleSnapping()
    {
        _savedScale = objectToSnap.localScale;
        _savedScale.x = ProcessOneAxisSnap(vector3Reference.x, transformReference.position.x, vector3DataReference.value.x);
        objectToSnap.localScale = _savedScale;
    }
    
    public void ApplyYOnlyScaleSnapping()
    {
        _savedScale = objectToSnap.localScale;
        _savedScale.z = ProcessOneAxisSnap(vector3Reference.y, transformReference.position.y, vector3DataReference.value.y);
        objectToSnap.localScale = _savedScale;
    }
    
    public void ApplyZOnlyScaleSnapping()
    {
        _savedScale = objectToSnap.localScale;
        _savedScale.z = ProcessOneAxisSnap(vector3Reference.z, transformReference.position.z, vector3DataReference.value.z);
        objectToSnap.localScale = _savedScale;
    }

    private float ProcessOneAxisSnap(float vectorAxis, float transformAxis, float vectorDataAxis)
    {
        switch (mode)
        {
            case Modes.SnapToVector3Reference:
                return vectorAxis;
                break;
            case Modes.SnapToTransformReference:
                return transformAxis;
                break;
            case Modes.SnapToVector3DataReference:
                return vectorDataAxis;
                break;
            default:
                return vectorAxis;
                break;
        }
    }
}
