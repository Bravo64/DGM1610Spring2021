using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Platformer2DBehaviour : MonoBehaviour
{
    public float groundedMovementSpeed, inAirMovementSpeed, jumpForce;

    [HideInInspector]
    public bool isGrounded = false;
    private Rigidbody2D _myRigidbody2D;
    private Vector3 _myVelocity;
    
    void Start()
    {
        _myRigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            _myVelocity = _myRigidbody2D.velocity;
            var horizontalInput = Input.GetAxis("Horizontal");
            if (isGrounded)
            {
                _myVelocity.x = horizontalInput * groundedMovementSpeed;
            }
            else
            {
                _myVelocity.x = horizontalInput * inAirMovementSpeed;
            }
            _myRigidbody2D.velocity = _myVelocity;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _myRigidbody2D.AddForce(Vector2.up * jumpForce);
            isGrounded = false;
        }
    }
}
