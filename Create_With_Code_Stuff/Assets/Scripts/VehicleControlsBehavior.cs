using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleControlsBehavior : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10.0f;
    [SerializeField]
    private float turnSpeed = 20.0f;
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
    }
}
