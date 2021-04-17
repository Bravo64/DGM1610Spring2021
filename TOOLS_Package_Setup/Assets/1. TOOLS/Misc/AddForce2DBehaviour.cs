using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AddForce2DBehaviour : MonoBehaviour
{
    [SerializeField] 
    private float amount;
    private enum Directions { X, Y }
    [SerializeField]
    private Directions alongAxes = Directions.X;

    private Rigidbody2D _myRigidbody2D;
    private Vector2 _direction;
    
    void Start()
    {
        _myRigidbody2D = GetComponent<Rigidbody2D>();

        switch (alongAxes)
        {
            case Directions.X:
                _direction = Vector2.right;
                break;
            case Directions.Y:
                _direction = Vector2.up;
                break;
            default:
                _direction = Vector2.right;
                break;
        }
    }
    
    void Update()
    {
        _myRigidbody2D.AddForce(amount * _direction);
    }
}
