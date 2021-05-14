using System.Collections;
using UnityEngine;

public class SmoothRotationBehaviour : MonoBehaviour
{
    public enum Modes { UseObjectRotation, UseEulerAngleRotation}
    public Modes mode = Modes.UseObjectRotation;
    public Transform targetRotationObject;
    public bool eulerIsRelative = false;
    public Vector3 eulerAngles;
    public float turningRate = 90f;

    private Quaternion _convertedEulerAngles;

    private void Start()
    {
        if (mode == Modes.UseObjectRotation)
        {
            StartCoroutine(FollowObjectRotation());
        }
        else
        {
            StartCoroutine(RotateToEulerAngles());
        }
    }

    IEnumerator FollowObjectRotation()
    {
        while (true)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotationObject.rotation, turningRate * Time.deltaTime);
            yield return 0;
        }
    }
    
    IEnumerator RotateToEulerAngles()
    {
        if (eulerIsRelative)
        {
            _convertedEulerAngles = Quaternion.Euler(eulerAngles + transform.eulerAngles);
        }
        else
        {
            _convertedEulerAngles = Quaternion.Euler(eulerAngles);
        }
        
        while (transform.rotation != _convertedEulerAngles)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _convertedEulerAngles, turningRate * Time.deltaTime);
            yield return 0;
        }
    }
}
