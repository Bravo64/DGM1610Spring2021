using UnityEngine;

public class SmoothFollowAxisBehaviour : MonoBehaviour
{
    public Transform targetObject;
    public bool followX, followY, followZ;
    public float smoothTime = 0.3F;
    
    private float _xVelocity, _yVelocity, _zVelocity;
    private Vector3 _currentPosition, _targetPosition;

    void FixedUpdate()
    {
        _currentPosition = transform.position;
        _targetPosition = targetObject.position;
        
        if (followX)
        {
            _currentPosition.x = Mathf.SmoothDamp(_currentPosition.x, _targetPosition.x, ref _xVelocity, smoothTime);
        }
        
        if (followY)
        {
            _currentPosition.y = Mathf.SmoothDamp(_currentPosition.y, _targetPosition.y, ref _yVelocity, smoothTime);
        }
        
        if (followZ)
        {
            _currentPosition.x = Mathf.SmoothDamp(_currentPosition.z, _targetPosition.z, ref _zVelocity, smoothTime);
        }
    }
}
