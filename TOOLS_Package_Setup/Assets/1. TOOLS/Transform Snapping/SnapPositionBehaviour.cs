using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPositionBehaviour : MonoBehaviour
{
    public enum Modes { SnapToVector3Reference, SnapToTransformReference, SnapToVector3DataReference }
    
    public Modes mode = Modes.SnapToVector3Reference;
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

    void ApplyPositionSnapping()
    {
        switch (mode)
        {
            case Modes.SnapToVector3Reference:
                transform.position = vector3Reference;
                break;
            case Modes.SnapToTransformReference:
                transform.position = transformReference.position;
                break;
            case Modes.SnapToVector3DataReference:
                transform.position = vector3DataReference.value;
                break;
            default:
                transform.position = vector3Reference;
                break;
        }
    }
}
