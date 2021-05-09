using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class DraggableRB : MonoBehaviour
{
    public float _alignmentSpeed = 550;
    
    private Rigidbody _myRigidbody;
    private Vector3 _screenPoint, _offset, _savedInputPos, _desiredPos, _direction;
    private bool _limitDragging = false;

    private void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        _screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        SaveInputPosition();
        _offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(_savedInputPos);
        _myRigidbody.useGravity = false;
    }

    void OnMouseDrag()
    {
        _myRigidbody.velocity = Vector3.zero;
        SaveInputPosition();
        _desiredPos = Camera.main.ScreenToWorldPoint(_savedInputPos) + _offset;
        _direction = (_desiredPos - transform.position).normalized;
        float distFromTarget = Vector3.Distance(transform.position, _desiredPos);
        if (_limitDragging)
        {
            distFromTarget /= 3;
        }
        _myRigidbody.AddForce(_direction * distFromTarget * _alignmentSpeed);
    }

    private void OnMouseUp()
    {
        _myRigidbody.velocity *= 0.6f;
        _myRigidbody.useGravity = true;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        _limitDragging = true;
    }

    private void OnCollisionExit(Collision other)
    {
        _limitDragging = false;
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