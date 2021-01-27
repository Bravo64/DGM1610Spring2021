using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controls : MonoBehaviour
{
    [SerializeField] private int rotationSpeed = 10;
    [SerializeField] private Rigidbody2D[] wheels;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            float rotation = Input.GetAxis("Horizontal") * -rotationSpeed;
            foreach (Rigidbody2D wheel in wheels)
            {
                wheel.AddTorque(rotation);
            }
        }
    }
}
