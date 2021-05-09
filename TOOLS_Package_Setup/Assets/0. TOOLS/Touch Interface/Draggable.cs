using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Draggable : MonoBehaviour
{
    private Vector3 _screenPoint, _offset, _savedInputPos, _currentPos, _velocity;
    private float _smoothTime = 0.003f;

    void OnMouseDown()
    {
        _screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        SaveInputPosition();
        _offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(_savedInputPos);
    }

    void OnMouseDrag()
    {
        SaveInputPosition();
        _currentPos = Camera.main.ScreenToWorldPoint(_savedInputPos) + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, _currentPos, ref _velocity, _smoothTime);
    }

    void SaveInputPosition()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            _savedInputPos.Set(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
        }
        else if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            _savedInputPos.Set(Input.touches[0].position.x, Input.touches[0].position.y, _screenPoint.z);
        }
    }
 
}