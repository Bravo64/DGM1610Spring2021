using UnityEngine;

public class SnapPositionBehaviour : MonoBehaviour
{
    public enum Modes { SnapToVector3Reference, SnapToTransformReference, SnapToVector3DataReference }
    public enum SnapAxes { SnapAllAxes, SnapXOnly, SnapYOnly, SnapZOnly }

    public Transform objectToSnap;
    public Modes mode = Modes.SnapToVector3Reference;
    public bool vectorIsRelative = false;
    public Vector3 vector3Reference;
    public Transform transformReference;
    public Vector3Data vector3DataReference;
    public SnapAxes snapAxesType = SnapAxes.SnapAllAxes;
    public bool runOnStart = true;

    private Vector3 _savedPos;
    
    void Start()
    {
        if (runOnStart)
        {
            switch (snapAxesType)
            {
                case SnapAxes.SnapAllAxes:
                    ApplyFullPositionSnapping();
                    break;
                case SnapAxes.SnapXOnly:
                    ApplyXOnlyPositionSnapping();
                    break;
                case SnapAxes.SnapYOnly:
                    ApplyYOnlyPositionSnapping();
                    break;
                case SnapAxes.SnapZOnly:
                    ApplyZOnlyPositionSnapping();
                    break;
            }
        }
    }

    public void ApplyFullPositionSnapping()
    {
        switch (mode)
        {
            case Modes.SnapToVector3Reference:
                if (vectorIsRelative)
                {
                    objectToSnap.position += vector3Reference;
                }
                else
                {
                    objectToSnap.position = vector3Reference;
                }
                break;
            case Modes.SnapToTransformReference:
                objectToSnap.position = transformReference.position;
                break;
            case Modes.SnapToVector3DataReference:
                objectToSnap.position = vector3DataReference.value;
                break;
        }
    }
    
    public void ApplyXOnlyPositionSnapping()
    {
        _savedPos = objectToSnap.position;
        _savedPos.x = ProcessOneAxisSnap(_savedPos.x, vector3Reference.x, transformReference.position.x, vector3DataReference.value.x);
        objectToSnap.position = _savedPos;
    }
    
    public void ApplyYOnlyPositionSnapping()
    {
        _savedPos = objectToSnap.position;
        _savedPos.y = ProcessOneAxisSnap(_savedPos.y, vector3Reference.y, transformReference.position.y, vector3DataReference.value.y);
        objectToSnap.position = _savedPos;
    }
    
    public void ApplyZOnlyPositionSnapping()
    {
        _savedPos = objectToSnap.position;
        _savedPos.z = ProcessOneAxisSnap(_savedPos.z, vector3Reference.z, transformReference.position.z, vector3DataReference.value.z);
        objectToSnap.position = _savedPos;
    }

    private float ProcessOneAxisSnap(float savedPosAxis, float vectorAxis, float transformAxis, float vectorDataAxis)
    {
        switch (mode)
        {
            case Modes.SnapToVector3Reference:
                if (vectorIsRelative) { return savedPosAxis + vectorAxis; }
                else { return vectorAxis; }
                break;
            case Modes.SnapToTransformReference:
                return transformAxis;
                break;
            case Modes.SnapToVector3DataReference:
                return vectorDataAxis;
                break;
            default:
                if (vectorIsRelative) { return savedPosAxis + vectorAxis; }
                else { return vectorAxis; }
                break;
        }
    }
}