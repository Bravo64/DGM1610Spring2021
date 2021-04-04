using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleControlsBehavior : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10.0f;
    [SerializeField]
    private float turnSpeed = 20.0f;
    [SerializeField]
    private float weaponForce = 100.0f;
    [SerializeField]
    private Transform weaponBarrel;
    [SerializeField]
    private Rigidbody projectile;
    Rigidbody _myRigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Vertical"))
        {
            transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime);
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
