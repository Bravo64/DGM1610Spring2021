using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controls : MonoBehaviour
{
    [SerializeField] private int rotationForce = 25;
    [SerializeField] private Rigidbody2D[] wheels;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("right"))
        {
            foreach (Rigidbody2D wheel in wheels)
            {
                wheel.AddTorque(-rotationForce);
            }
                
        }
        else if (Input.GetKey("left"))
        {
            foreach (Rigidbody2D wheel in wheels)
            {
                wheel.AddTorque(rotationForce * 0.5f);
            }
        }
    }
}
