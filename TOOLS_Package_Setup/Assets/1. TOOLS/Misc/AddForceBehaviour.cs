using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AddForceBehaviour : MonoBehaviour
{
    [SerializeField] 
    private float amount;
    private enum Directions { X, Y, Z }
    [SerializeField]
    private Directions alongAxes = Directions.X;

    private Rigidbody _myRigidbody;
    private Vector3 _direction;
    
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();

        switch (alongAxes)
        {
            case Directions.X:
                _direction = Vector3.right;
                break;
            case Directions.Y:
                _direction = Vector3.up;
                break;
            case Directions.Z:
                _direction = Vector3.forward;
                break;
            default:
                _direction = Vector3.right;
                break;
        }
    }
    
    void Update()
    {
        _myRigidbody.AddForce(amount * _direction);
    }
}
