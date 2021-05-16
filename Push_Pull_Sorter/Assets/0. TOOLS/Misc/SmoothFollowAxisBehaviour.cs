using UnityEngine;

public class SmoothFollowAxisBehaviour : MonoBehaviour
{
    public Transform targetObject;
    public bool followX, followY, followZ;
    public float smoothTime = 0.3F;
    
    private float _xVelocity, _yVelocity, _zVelocity;
    private Vector3 _currentPos, _targetPos;

    void FixedUpdate()
    {
        _currentPos = transform.position;
        _targetPos = targetObject.position;
        
        if (followX)
        {
            _currentPos.x = Mathf.SmoothDamp(_currentPos.x, _targetPos.x, ref _xVelocity, smoothTime);
        }
        
        if (followY)
        {
            _currentPos.y = Mathf.SmoothDamp(_currentPos.y, _targetPos.y, ref _yVelocity, smoothTime);
        }
        
        if (followZ)
        {
            _currentPos.x = Mathf.SmoothDamp(_currentPos.z, _targetPos.z, ref _zVelocity, smoothTime);
        }
    }
}
