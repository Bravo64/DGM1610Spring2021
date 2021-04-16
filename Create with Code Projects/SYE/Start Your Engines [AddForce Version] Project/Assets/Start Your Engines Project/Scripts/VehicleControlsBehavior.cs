using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VehicleControlsBehavior : MonoBehaviour
{
    [SerializeField]
    private float horsePower = 10.0f;
    [SerializeField]
    private float turnSpeed = 20.0f;
    [SerializeField]
    private float weaponForce = 100.0f;
    [SerializeField]
    private Transform weaponBarrel;
    [SerializeField]
    private Rigidbody projectile;
    [SerializeField]
    private Transform centerOfMass;
    Rigidbody _myRigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
        _myRigidbody.centerOfMass = centerOfMass.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Vertical"))
        {
            float verticalInput = Input.GetAxis("Vertical");
            _myRigidbody.AddRelativeForce(Vector3.forward * (verticalInput * horsePower));
        }
        
        if (Input.GetButton("Horizontal"))
        {
            float direction;
            if (Input.GetAxis("Vertical") < 0)
            {
                direction = -1;
            }
            else if (Input.GetAxis("Vertical") == 0)
            {
                direction = 0;
            }
            else
            {
                direction = 1;
            }
                transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * turnSpeed * direction * Time.deltaTime);
        }
        
        if (Input.GetButtonDown("Jump"))
        {
            Rigidbody projectileRB = Instantiate(projectile, weaponBarrel.position, weaponBarrel.rotation);
            projectileRB.velocity = transform.forward * weaponForce;
        }
    }
}
