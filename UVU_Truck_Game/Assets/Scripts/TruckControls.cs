﻿using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class TruckControls : MonoBehaviour
{
    /*
    ---------------- Documentation ---------------------
    
    Script's Name: TruckControls.cs
    
    Script's Description: This script deals with moving the
        truck based on user input, and draining the fuel 
        supply accordingly. It also freezes the truck's
        rigidbody, and disables this script when the
        fuel is empty (sending controls to the next car).
        
    Script's Methods:
        - Start
        - Update
        - DrainFuel
        - OutOfFuel (Coroutine)
        - RestoreFuel (public)
        - CheckForAssignmentErrors
    
    --------------------- DOC END ----------------------
     */
    
    //----------------- Public Variables -------------------
    
    // Car ID Number (Determines car activation order)
    [Header("Car ID Number (Order of Activations):")]
    public int carIDNumber = 0;
    
    //------------------------------------------------------
    
    
    //----- Serialized Variables (private, shows in Editor) -----
    
    // Move Sensitivity.
    [Header("Forward/backward Senensitivity:")]
    [SerializeField] private int wheelSpeed = 10;
    // Tilt Sensitivity.
    [Header("Senensitivity of truck 'spin':")]
    [SerializeField] private int tiltSensitivity = 25;
    // Time left on fuel.
    [Header("(when gas is pressed):")]
    [Header("Time before fuel is empty...")]
    [SerializeField] private float secondsOfFuel = 20.0f;
    
    //-------------------------------------------------------


    //---------------- Private Variables -------------------
    
    // List for the wheels of the car
    private List<Rigidbody2D> _wheels = new List<Rigidbody2D>();
    
    // Boolean telling when we are out of fuel.
    private bool _fuelIsEmpty;
    
    // The truck's Rigidbody2D Component.
    private Rigidbody2D _myRigidbody2D;

    // Color strip that tells the player
    // how much fuel they have left.
    private Transform _fuelColorStrip;

    // The Sprite Renderer of the fuel strip.
    private SpriteRenderer _fuelStripRenderer;
    
    // The Main Camera (Cinemachine Virtual Camera).
    private CinemachineVirtualCamera _mainCamera;
    
    // Array of every car in the scene.
    private GameObject[] _allCars;
    
    // The Default amount (in seconds) of a full tank of fuel.
    private float _defaultFuelAmount;
    
    // The Default color of a full tank of fuel.
    private Color _fullTankColor;

    //-----------------------------------------------------


    //-------------- The Start Method -------------------
    // This Method is called before the first frame update
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
                    _wheels.Add(child2.GetComponent<Rigidbody2D>());
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

        // Grab the virtual camera object
        GameObject camObject = GameObject.Find("/Virtual_Cam");
        if (camObject)
        {
            // Get the main camera component (Cinemachine Virtual Camera).
            _mainCamera = camObject.GetComponent<CinemachineVirtualCamera>();
        }

        // Collect all cars in the scene ("Vehicle" tag).
        _allCars = GameObject.FindGameObjectsWithTag("Vehicle");
        
        // This Method will double check that everything
        // was located and assigned properly.
        CheckForAssignmentErrors();
    }

    
    //------- The Update Method ------------
    // This Method is called once per frame.
    // For this script, it mainly checks for
    // player input, and acts accordingly.
    //--------------------------------------
    void Update()
    {
        // Check that the fuel's not empty.
        // If it is, end the Method.
        if (_fuelIsEmpty)
        {
            return;
        }
        // Check that the Player is
        // pressing Up or Down Arrow.
        if (Input.GetAxis("Vertical") != 0)
        {
            // Set up the rotation variable in
            // preparation for applied angular force
            // (Multiply Axis by sensitivity variable).
            float rotation = Input.GetAxis("Vertical") * -wheelSpeed;
            // Access each wheel in the wheels list
            foreach (Rigidbody2D wheel in _wheels)
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

    
    //------- The DrainFuel Method ------------
    // This is called when the player moves and
    // we need the fuel to go down. It deals with
    // draining the "secondsOfFuel" variable and
    // changing the size and color of the
    // fuel color strip.
    //----------------------------------------
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
            // Call the OutOfFuel Coroutine
            StartCoroutine(OutOfFuel());
        }
        
        // Get the fuel strip's scale
        Vector3 fuelStripScale = _fuelColorStrip.localScale;

        // Shrink the fuel strip on the x axis
        // until it reaches (almost) zero.
        if (_fuelColorStrip.localScale.x > 0.01)
        {
            // Calculate the shrink speed based on the scale remaining, and 
            // amount of fuel time left.
            float shrinkSpeed = (_fuelColorStrip.localScale.x / secondsOfFuel);
            // Shrink on the X Axis.
            fuelStripScale.x -= shrinkSpeed * Time.deltaTime;
            // Get the color.
            Color spriteColor = _fuelStripRenderer.color;
            // Manipulate the color over time from green to red (using RGB).
            spriteColor.g -= (spriteColor.g / secondsOfFuel) * Time.deltaTime;
            spriteColor.b -= (spriteColor.b / secondsOfFuel) * Time.deltaTime;
            spriteColor.r += (spriteColor.r / secondsOfFuel) * Time.deltaTime * 5;
            // Reassign the color.
            _fuelStripRenderer.color = spriteColor;
        }
        else if (fuelStripScale.x < 0.01)
        {
            // If the scale passes 0.01,
            // reset it to zero (x axis)
            // and end the script.
            fuelStripScale.x = 0.0f;
        }
        // Reassign the fuel strip's scale
        _fuelColorStrip.localScale = fuelStripScale;
    }
    
    
    //---------- The OutOfFuel Coroutine -------------
    // This Coroutine deals with disabling this script
    // and the truck rigidbodies when the truck runs 
    // out of fuel. It waits a few seconds
    // before doing so.
    //--------------------------------------------
    IEnumerator OutOfFuel()
    {
        // Let other Methods know the fuel
        // is gone by setting this to true.
        _fuelIsEmpty = true;
        // Wait a second to switch vehicles.
        yield return new WaitForSeconds(1.0f);
        // Look through all cars in the scene
        foreach (var car in _allCars)
        {
            // Get their car script.
            TruckControls carsScript = car.GetComponent<TruckControls>();
            // Check if they are the next vehicle based on their ID.
            if (carsScript.carIDNumber == carIDNumber + 1)
            {
                // Enabled their controls (script).
                carsScript.enabled = true;
                // Set the virtual camera to follow them.
                _mainCamera.Follow = car.transform;
            }
        }
        // Give the car a few seconds to settle.
        yield return new WaitForSeconds(1.5f);
        // while loop continuously checks for car movement.
        while (!_myRigidbody2D.isKinematic)
        {   
            // Check that we are not moving (almost).
            if (_myRigidbody2D.velocity.x < 0.005f && 
                _myRigidbody2D.velocity.y < 0.005f && 
                _myRigidbody2D.angularVelocity < 0.005f)
            {
                // "isKinematic" will freeze the car's rigidbody.
                _myRigidbody2D.isKinematic = true;
                // Add constraints just in case
                _myRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
                foreach (var wheel in _wheels)
                {
                    // Freeze the wheels as well.
                    wheel.isKinematic = true;
                    // And freeze the wheel's axel (its parent).
                    wheel.transform.parent.GetComponent<Rigidbody2D>().isKinematic = true;
                }
                // Leave the while loop.
                break;
            }
            else
            {
                // If it's still moving, wait another second
                // (while loop resets).
                yield return new WaitForSeconds(1.0f);
            }
        }
        // Disable this script.
        this.enabled = false;
    }

    //------- The RestoreFuel Method ------------
    // This Method is called by the "Fuel Item
    // Pickup" object when the car triggers its
    // collider. This Method deals with restoring
    // the fuel amount, fuel meter, and rigidbodies
    // to their default state.
    //----------------------------------------
    public void RestoreFuel()
    {
        // Reactivate the trucks rigidbody
        _myRigidbody2D.constraints = RigidbodyConstraints2D.None;
        foreach (var wheel in _wheels)
        {
            // Let the other methods know the fuel is back with this variable.
            _fuelIsEmpty = false;
            // Restore the fuel (in seconds) to the full default value.
            secondsOfFuel = _defaultFuelAmount;
            // Unfreeze the wheels as well.
            wheel.isKinematic = false;
            // And Unfreeze the wheel's axel (its parent).
            wheel.transform.parent.GetComponent<Rigidbody2D>().isKinematic = false;
            // Reset the color of the fuel meter.
            _fuelStripRenderer.color = _fullTankColor;
            // Reset the size of the fuel meter (on x axis).
            Vector3 fuelMeterScale = _fuelColorStrip.localScale;
            fuelMeterScale.x = 1.0f;
            _fuelColorStrip.localScale = fuelMeterScale;
        }
    }

    //----- The CheckForAssignmentErrors Method --------
    // This Method checks that everything was correctly
    // located and assigned within the Start Method.
    // If anything was not properly located, an error
    // message will appear in the console and this
    // script will disable itself.
    //-------------------------------------------------
    void CheckForAssignmentErrors()
    {
        // Double check that
        // we got the wheels.
        if (_wheels.Count < 2)
        {
            // Print error message if not, and stop the script
            Debug.Log("Error: Missing (2) children with 'Wheel' tag. " +
                      "\t(/Truck --> Front or Back_Wheel_Anchor --> Front/Back_Wheel)");
            this.enabled = false;
        }

        // Double check that we
        // got the fuel strip.
        if (!_fuelColorStrip)
        {
            // Print error message if not, and stop the script
            Debug.Log("Error: 'Fuel_Color_Strip' child is missing. " +
                      "\t(/Truck --> Fuel_Meter --> Fuel_Color_Strip --> Color_Strip)");
            this.enabled = false;
        }

        // Now that we know it's there, get the fuel
        // color strip's sprite piece from its children,
        // so that we can change its color later on.
        foreach (Transform child3 in _fuelColorStrip)
        {
            // Check for its name
            if (child3.name == "Color_Strip")
            {
                // Grab the SpriteRenderer Component.
                _fuelStripRenderer = child3.GetComponent<SpriteRenderer>();
                // If tank is not empty.
                if (secondsOfFuel != 0)
                {
                    // If the fuel is not empty, save it as
                    // the default (full) fuel meter color.
                    _fullTankColor = _fuelStripRenderer.color;
                    // Also save its fuel amount as the default value.
                    _defaultFuelAmount = secondsOfFuel;
                }
            }
        }
        // Double check that we have
        // a fuel Strip Sprite Renderer.
        if (!_fuelStripRenderer)
        {
            // Print error message if not, and stop the script
            Debug.Log("Error: Missing 'Color_Strip' SpriteRenderer Component. " +
                      "\t(/Truck --> Fuel_Meter --> Fuel_Color_Strip --> Color_Strip)");
            this.enabled = false;
        }

        // Double check that we have
        // a Rigidbody2D Component.
        if (!_myRigidbody2D)
        {
            // Print error message if not, and stop the script
            Debug.Log("Error: Truck missing 'Rigidbody2D' component.");
            this.enabled = false;
        }
        
        // Double check that we have a
        // Main Virtual Camera Component.
        if (!_mainCamera)
        {
            // Print error message if not, and stop the script
            Debug.Log("Error: Main 'Cinemachine' Virtual " +
                      "Camera Component is missing (/'Virtual_Cam')");
            this.enabled = false;
        }
        
        // Double check we have collected
        // the cars in the scene.
        if (_allCars.Length == 0)
        {
            // Print error message if not, and stop the script
            Debug.Log("Error: Cars (Trucks) with 'Vehicle' tag are missing");
            this.enabled = false;
        }
    }
}
// ---------------------- END OF FILE -----------------------