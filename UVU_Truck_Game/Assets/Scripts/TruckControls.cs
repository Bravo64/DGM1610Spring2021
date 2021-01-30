using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckControls : MonoBehaviour
{
    /*
    ---------------- Documentation ---------------------
    
    Script's Name: TruckControls.cs
    
    Script's Description: This script deals with moving the
        truck based on user input, and draining the fuel 
        supply accordingly.
        
    Script's Functions:
        - Start
        - Update
        - DrainFuel
        - CheckForAssignmentErrors
    
    --------------------- DOC END ----------------------
     */
    
    //------ Serialized Variables (Private, shows in Editor) -----
    
    [Header("Forward/backward Senensitivity:")]
    [SerializeField] private int wheelSpeed = 10;
    [Space]
    [Header("Senensitivity of truck 'spin':")]
    [SerializeField] private int tiltSensitivity = 25;
    // List of truck wheels.
    List<Rigidbody2D> wheels = new List<Rigidbody2D>();
    [Header("(when gas is pressed):")]
    [Header("Time before fuel is empty...")]
    [SerializeField] private float secondsOfFuel = 20.0f;
    
    //-------------------------------------------------------


    //---------------- Private Variables -------------------
    
    // The truck's Rigidbody2D Component.
    private Rigidbody2D _myRigidbody2D;

    // Color strip that tells the player
    // how much fuel they have left.
    private Transform _fuelColorStrip;

    // The Sprite Renderer of the fuel strip.
    private SpriteRenderer _fuelStripRenderer;
    
    //-----------------------------------------------------


    //-------------- The Start Function -------------------
    // This function is called before the first frame update
    // (or at the gameObject's creation/reactivation). It
    // is mainly used for setup and collecting the truck's
    // children that we need.
    //-----------------------------------------------------
    void Start()
    {
        // Loop through the truck's children.
        foreach (Transform child in transform)
        {
            // Find the wheels in the
            // children of the children.
            foreach (Transform child2 in child)
            {
                // Check for "Wheel" Tag.
                if (child2.CompareTag("Wheel"))
                {
                    // Get the wheel Rigidbody2D Component,
                    // and add it to the wheels list.
                    wheels.Add(child2.GetComponent<Rigidbody2D>());
                }
                // Else, check for the fuel strip's name.
                else if (child2.name == "Fuel_Color_Strip")
                {
                    // Save the fuel strip transform.
                    _fuelColorStrip = child2.transform;
                }
            }
        }

        // Get the truck's Rigidbody2D Component.
        _myRigidbody2D = transform.GetComponent<Rigidbody2D>();

        // This function will double check that everything
        // was located and assigned properly.
        CheckForAssignmentErrors();
    }

    //------- The Update Function ------------
    // This Function is called once per frame.
    // For this script, it mainly checks for
    // player input, and acts accordingly.
    //--------------------------------------
    void Update()
    {
        // Check that the Player is
        // pressing Up or Down Arrow.
        if (Input.GetAxis("Vertical") != 0)
        {
            // Set up the rotation variable in
            // preparation for applied angular force
            // (Multiply Axis by sensitivity variable).
            float rotation = Input.GetAxis("Vertical") * -wheelSpeed;
            // Access each wheel in the wheels list
            foreach (Rigidbody2D wheel in wheels)
            {
                // Turn this wheel
                wheel.AddTorque(rotation);
                DrainFuel();
            }
        }

        // Check that the Player is pressing
        // Left or Right Arrow.
        if (Input.GetAxis("Horizontal") != 0)
        {
            // Set up the rotation variable in
            // preparation for applied angular force
            // (Multiply Axis by sensitivity variable).
            float rotation = Input.GetAxis("Horizontal") * -tiltSensitivity;
            // Turn (tilt) the whole truck
            _myRigidbody2D.AddTorque(rotation);
        }
    }

    //------- The DrainFuel Function ------------
    // This is called when the player moves and
    // we need the fuel to go down. It deals with
    // draining the "secondsOfFuel" variable and
    // changing the size and color of the
    // fuel color strip.
    //--------------------------------------
    void DrainFuel()
    {
        if (secondsOfFuel > 0)
        {
            // Subtract the passage of time
            // from the secondsOfFuel variable.
            secondsOfFuel -= Time.deltaTime;
        }
        else if (secondsOfFuel < 0)
        {
            // If secondsOfFuel passes zero,
            // reset it back to zero.
            secondsOfFuel = 0;
        }

        Vector3 fuelStripScale = _fuelColorStrip.localScale;

        // Shrink the fuel strip on the x axis
        // until it reaches (almost) zero.
        if (_fuelColorStrip.localScale.x > 0.01)
        {
            float shrinkSpeed = (_fuelColorStrip.localScale.x / secondsOfFuel);
            fuelStripScale.x -= shrinkSpeed * Time.deltaTime;
            Color spriteColor = _fuelStripRenderer.color;
            spriteColor.g -= (spriteColor.g / secondsOfFuel) * Time.deltaTime;
            spriteColor.b -= (spriteColor.b / secondsOfFuel) * Time.deltaTime;
            spriteColor.r += (spriteColor.r / secondsOfFuel) * Time.deltaTime * 5;
            _fuelStripRenderer.color = spriteColor;
        }
        // If the scale passes 0.01,
        // reset it to zero (x axis)
        // and end the script.
        else if (fuelStripScale.x < 0.01)
        {
            fuelStripScale.x = 0.0f;
            this.enabled = false;
        }

        _fuelColorStrip.localScale = fuelStripScale;
    }
    
    //----- The CheckForAssignmentErrors Function --------
    // This function checks that everything was correctly
    // located and assigned within the Start function.
    // If anything was not properly located, an error
    // message will appear in the console and this
    // script will disable itself.
    //---------------------------------------------------
    void CheckForAssignmentErrors()
    {
        // Double check that
        // we got the wheels.
        if (wheels.Count < 2)
        {
            // Print error message if not, and stop the script
            Debug.Log("Error: Missing 2 children with 'Wheel' tag.");
            this.enabled = false;
        }

        // Double check that we
        // got the fuel strip.
        if (_fuelColorStrip == false)
        {
            // Print error message if not, and stop the script
            Debug.Log("Error: 'Fuel_Color_Strip' child is missing.");
            this.enabled = false;
        }

        // Get the fuel color strip's sprite piece
        // from its children, so that we can change
        // its color later on.
        foreach (Transform child3 in _fuelColorStrip)
        {
            // Check for its name
            if (child3.name == "Color_Strip")
            {
                // Grab the SpriteRenderer Component.
                _fuelStripRenderer = child3.GetComponent<SpriteRenderer>();
            }
        }
        // Double check that we have
        // a fuel Strip Sprite Renderer.
        if (_fuelStripRenderer == false)
        {
            // Print error message if not, and stop the script
            Debug.Log("Error: Missing 'Color_Strip' SpriteRenderer Component.");
            this.enabled = false;
        }

        // Double check that we have
        // a Rigidbody2D Component.
        if (_myRigidbody2D == false)
        {
            // Print error message if not, and stop the script
            Debug.Log("Error: Truck missing 'Rigidbody2D' component.");
            this.enabled = false;
        }
    }
}
// -------------------------- END OF FILE ---------------------------