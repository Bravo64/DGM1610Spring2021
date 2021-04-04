using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX1 : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float propRotationSpeed = 720.0f;
    [SerializeField] 
    private float weaponForce = 100.0f;
    [SerializeField]
    private Transform propeller;
    [SerializeField]
    private Rigidbody bullet;
    [SerializeField]
    private Transform weaponLeft;
    [SerializeField]
    private Transform weaponRight;
    private float verticalInput;
    private float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // get the user's vertical input
        verticalInput = Input.GetAxis("Vertical");
        // get the user's horizontal input
        horizontalInput = Input.GetAxis("Horizontal");

        // move the plane forward at a constant rate
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // tilt the plane up/down based on up/down arrow keys
        transform.Rotate(Vector3.right * verticalInput * rotationSpeed * Time.deltaTime);
        
        propeller.transform.Rotate(Vector3.forward * propRotationSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump"))
        {
            Rigidbody bulletRB1 = Instantiate(bullet, weaponLeft.position, weaponLeft.rotation);
            bulletRB1.velocity = transform.forward * weaponForce;
            Rigidbody bulletRB2 = Instantiate(bullet, weaponRight.position, weaponRight.rotation);
            bulletRB2.velocity = transform.forward * weaponForce;
        }
    }
}
