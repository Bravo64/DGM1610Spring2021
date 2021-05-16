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
        switch (mode)
        {
            case Modes.SnapToVector3Reference:
                if (vectorIsRelative) { _savedPos.x =  _savedPos.x + vector3Reference.x; }
                else { _savedPos.x = vector3Reference.x; }
                break;
            case Modes.SnapToTransformReference:
                _savedPos.x = transformReference.position.x;
                break;
            case Modes.SnapToVector3DataReference:
                _savedPos.x = vector3DataReference.value.x;
                break;
        }
        objectToSnap.position = _savedPos;
    }
    
    public void ApplyYOnlyPositionSnapping()
    {
        _savedPos = objectToSnap.position;
        switch (mode)
        {
            case Modes.SnapToVector3Reference:
                if (vectorIsRelative) { _savedPos.y =  _savedPos.y + vector3Reference.y; }
                else { _savedPos.y = vector3Reference.y; }
                break;
            case Modes.SnapToTransformReference:
                _savedPos.y = transformReference.position.y;
                break;
            case Modes.SnapToVector3DataReference:
                _savedPos.y = vector3DataReference.value.y;
                break;
        }
        objectToSnap.position = _savedPos;
    }
    
    public void ApplyZOnlyPositionSnapping()
    {
        _savedPos = objectToSnap.position;
        switch (mode)
        {
            case Modes.SnapToVector3Reference:
                if (vectorIsRelative) { _savedPos.z =  _savedPos.z + vector3Reference.z; }
                else { _savedPos.z = vector3Reference.z; }
                break;
            case Modes.SnapToTransformReference:
                _savedPos.z = transformReference.position.z;
                break;
            case Modes.SnapToVector3DataReference:
                _savedPos.z = vector3DataReference.value.z;
                break;
        }
        objectToSnap.position = _savedPos;
    }
}