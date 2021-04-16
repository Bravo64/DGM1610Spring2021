using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    float speed;
    float rpm;
    [SerializeField] private float horsePower = 0;
    [SerializeField] float turnSpeed = 30.0f;
    
    [SerializeField] GameObject centerOfMass;
    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] TextMeshProUGUI rpmText;

    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField ]int wheelsOnGround;
    
    private float horizontalInput;
    private float forwardInput;
    private Rigidbody _myRigidbody;


    private void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
        _myRigidbody.centerOfMass = centerOfMass.transform.position;
    }

    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        if (IsOnGround())
        {
            _myRigidbody.AddRelativeForce(Vector3.forward * forwardInput * horsePower);
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);
            
            speed = Mathf.RoundToInt(_myRigidbody.velocity.magnitude * 2.237f);
            speedometerText.SetText("Speed: " + speed + " mph");

            rpm = Mathf.Round((speed % 30) * 40);
            rpmText.SetText("RPM: " + rpm);
        }
    }

    bool IsOnGround()
    {
        wheelsOnGround = 0;
        foreach (WheelCollider wheel in allWheels)
        {
            if (wheel.isGrounded)
            {
                print("grounded");
                wheelsOnGround++;
            }
        }
        if (wheelsOnGround >= 2)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
