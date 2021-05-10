using UnityEngine;

public class SmoothFollowBehaviour : MonoBehaviour
{
    public Transform targetObject;
    public float smoothTime = 0.3F;
    
    private Vector3 _moveVelocity;

    void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetObject.position, ref _moveVelocity, smoothTime);
    }
}
