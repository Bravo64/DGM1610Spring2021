using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] 
    private float movementSpeed = 9.0f;
    [SerializeField]
    private float jumpForce = 900.0f;
    public Animator spriteAnimator;
    
    [HideInInspector]
    public bool isGrounded = false;
    private float currentSpeed;
    private Rigidbody2D _myRigidbody2D;
    private Vector3 _myVelocity;
    
    void Start()
    {
        currentSpeed = movementSpeed;
        _myRigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        _myVelocity = _myRigidbody2D.velocity;
        if (Input.GetButton("Horizontal"))
        {
            var horizontal = Input.GetAxis("Horizontal");
            _myVelocity.x = horizontal * currentSpeed;
            var myScale = transform.localScale;
            if (horizontal > 0)
            {
                myScale.x = math.abs(myScale.x);
            }
            else
            {
                myScale.x = -math.abs(myScale.x);
            }
            transform.localScale = myScale;
            _myRigidbody2D.velocity = _myVelocity;
            spriteAnimator.SetBool("walking", true);
        }
        
        if (Input.GetButtonUp("Horizontal"))
        {
            var horizontal = Input.GetAxis("Horizontal");
            _myVelocity.x = (horizontal * currentSpeed) * 0.6f;
            _myRigidbody2D.velocity = _myVelocity;
            spriteAnimator.SetBool("walking", false);
        }
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _myRigidbody2D.AddForce(Vector2.up * jumpForce);
            isGrounded = false;
        }
    }
}
