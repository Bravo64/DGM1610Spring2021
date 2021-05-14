using UnityEngine;

public class SnapScaleBehaviour : MonoBehaviour
{
    public enum Modes { SnapToVector3Reference, SnapToTransformReference, SnapToVector3DataReference }

    public Transform objectToSnap;
    public Modes mode = Modes.SnapToVector3Reference;
    public Vector3 vector3Reference;
    public Transform transformReference;
    public Vector3Data vector3DataReference;
    public bool runOnStart = true;

    void Start()
    {
        if (runOnStart)
        {
            ApplyScaleSnapping();
        }
    }

    void ApplyScaleSnapping()
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
}
