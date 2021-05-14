using System.Collections;
using UnityEngine;

public class PositionClampBehaviour : MonoBehaviour
{
    public enum Axes { X, Y, Z }
    public enum Modes { ConstantClamp, OnTriggerStayClamp }

    public Transform objectToClamp;
    public float clampMinimum, clampMaximum;
    public Axes clampAlongAxis = Axes.X;
    public Modes mode = Modes.ConstantClamp;

    public Vector3 _savedPos;
    
    void Start()
    {
        if (mode == Modes.ConstantClamp)
        {
            switch (clampAlongAxis)
            {
                case Axes.X:
                    StartCoroutine(ConstantXClamp());
                    break;
                case Axes.Y:
                    StartCoroutine(ConstantYClamp());
                    break;
                case Axes.Z:
                    StartCoroutine(ConstantZClamp());
                    break;
            }
        }
    }

    IEnumerator ConstantXClamp()
    {
        while (true)
        {
            ClampXOneFrame();
            yield return 0;
        }
    }
    
    IEnumerator ConstantYClamp()
    {
        while (true)
        {
            ClampYOneFrame();
            yield return 0;
        }
    }
    
    IEnumerator ConstantZClamp()
    {
        while (true)
        {
            ClampZOneFrame();
            yield return 0;
        }
    }

    public void ClampXOneFrame()
    {
        _savedPos = objectToClamp.position;
        _savedPos.x = Mathf.Clamp(_savedPos.x, clampMinimum, clampMaximum);
        objectToClamp.position = _savedPos;
    }
    
    public void ClampYOneFrame()
    {
        _savedPos = objectToClamp.position;
        _savedPos.y = Mathf.Clamp(_savedPos.y, clampMinimum, clampMaximum);
        objectToClamp.position = _savedPos;
    }
    
    public void ClampZOneFrame()
    {
        _savedPos = objectToClamp.position;
        _savedPos.z = Mathf.Clamp(_savedPos.z, clampMinimum, clampMaximum);
        objectToClamp.position = _savedPos;
    }

    private void OnTriggerStay(Collider other)
    {
        switch (clampAlongAxis)
        {
            case Axes.X:
                ClampXOneFrame();
                break;
            case Axes.Y:
                ClampYOneFrame();
                break;
            case Axes.Z:
                ClampZOneFrame();
                break;
        }
    }
}
