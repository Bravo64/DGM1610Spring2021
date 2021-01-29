using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckControls : MonoBehaviour
{
    // Sensitivity of the forward/backward controls
    [SerializeField] private int wheelSpeed = 10;
    // Sensitivity of the truck tilting (spinning) controls
    [SerializeField] private int tiltSensitivity = 25;
    // Random color to choose from
    [SerializeField] private Color[] colorOptions;
    // List of truck wheels
    List<Rigidbody2D> wheels  = new List<Rigidbody2D>();
    // The truck's Rigidbody2D Component
    private Rigidbody2D _myRigidbody2D;

    // Start Function is called before the first frame update
    // (or at the gameObject's creation/reactivation)
    void Start()
    {
        // Loop through the truck's children
        foreach (Transform child in transform)
        {
            // Find the wheels in the children of the children
            foreach (Transform child2 in child)
            {
                // Check for "Wheel" Tag
                if (child2.CompareTag("Wheel"))
                {
                    // Get the wheel Rigidbody2D Component, and add it to the wheels list
                    wheels.Add(child2.GetComponent<Rigidbody2D>());
                }
                else if (child2.name == "ColoredPiece")
                {
                    child2.GetComponent<SpriteRenderer>().color = colorOptions[Random.Range(0, colorOptions.Length)];
                }

            }
        }
        // Get the truck's Rigidbody2D Component
            _myRigidbody2D = transform.GetComponent<Rigidbody2D>();
    }

    // Update Function is called once per frame
    void Update()
    {
        // Check that the Player is pressing Up or Down Arrow
        if (Input.GetAxis("Vertical") != 0)
        {
            // Set up the rotation variable in preparation for applied angular force
            // (Multiply Axis by sensitivity variable)
            float rotation = Input.GetAxis("Vertical") * -wheelSpeed;
            // Access each wheel in the wheels list
            foreach (Rigidbody2D wheel in wheels)
            {
                // Turn this wheel
                wheel.AddTorque(rotation);
            }
        }
        
        // Check that the Player is pressing Left or Right Arrow
        if (Input.GetAxis("Horizontal") != 0)
        {
            // Set up the rotation variable in preparation for applied angular force
            // (Multiply Axis by sensitivity variable)
            float rotation = Input.GetAxis("Horizontal") * -tiltSensitivity;
            // Turn (tilt) the whole truck
            _myRigidbody2D.AddTorque(rotation);
        }
    }
}
