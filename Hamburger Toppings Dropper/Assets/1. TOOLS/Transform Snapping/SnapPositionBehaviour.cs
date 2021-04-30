using UnityEngine;

public class SnapPositionBehaviour : MonoBehaviour
{
    public enum Modes { SnapToVector3Reference, SnapToTransformReference, SnapToVector3DataReference }

    public Transform objectToSnap;
    public Modes mode = Modes.SnapToVector3Reference;
    public bool vectorIsRelative = false;
    public Vector3 vector3Reference;
    public Transform transformReference;
    public Vector3Data vector3DataReference;
    public bool runOnStart = true;

    void Start()
    {
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
            default:
                objectToSnap.position = vector3Reference;
                break;
        }
    }
}