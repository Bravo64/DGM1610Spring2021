﻿using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using GameEvents;
using Unity.Mathematics;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class TruckControls : MonoBehaviour
{
    /*
    ---------------- Documentation ---------------------
    
    Script's Name: TruckControls.cs
    Author: Keali'i Transfield
    
    ----------------------------------------------------
     */

    //---------- Public and Static Variables (visible in inspector)-----------

    [Header("----------- VALUE VARIABLES -----------", order = 0)] [Space(10, order = 1)]

    // Car ID Number (Determines car activation order, 1 is main)
    [UnityEngine.Range(30, 1)]
    public int carImportance = 1;

    // Move Sensitivity.
    [UnityEngine.Range(0, 50)] [SerializeField]
    private int wheelSpeed = 20;

    // Tilt Sensitivity.
    [UnityEngine.Range(0, 500)] [SerializeField]
    private int tiltSensitivity = 60;

    // Time left on fuel.
    [UnityEngine.Range(0.0f, 60.0f)] [SerializeField]
    private float secondsOfFuel = 1.8f;

    // Boosting can cause the wheel to catch and
    // spin the car forward. To correct this, we
    // will add torque based on this variable.
    [UnityEngine.Range(0, 10000f)] [SerializeField]
    private int boostAngularForce = 5000;

    [Header("-------------- BOOLEANS --------------", order = 2)] [Space(10, order = 3)]

    // Is the fuel meter attached to
    // the side of the truck, or part of the UI?
    [SerializeField]
    private bool fuelMeterIsAttached = false;

    [Header("---------------- CHILDREN ----------------", order = 4)] [Space(10, order = 5)]

    // The AudioSource component of the Game
    // Object that has the impact sound effect.
    [SerializeField]
    private AudioSource impactAudio;

    // The AudioSource component of the Game
    // Object that has the acceleration sound effect.
    [SerializeField] private AudioSource accelerationAudio;

    // The transform of the super speed blue trail particle object
    [SerializeField] private Transform yellowTrailParticle;

    // List for the wheel car axles (Rigidbody2D).
    [SerializeField] private Rigidbody2D[] wheelAxles;

    [Header("------------- GRANDCHILDREN --------------", order = 6)] [Space(10, order = 7)]

    // List for the wheels of the car (Rigidbody2D)
    [SerializeField]
    private Rigidbody2D[] wheels;

    [Header("---------------- FUEL UI -----------------", order = 8)] [Space(10, order = 9)]

    // Color strip that tells the player
    // how much fuel they have left.
    [SerializeField]
    private Transform fuelColorStrip;

    // The Image UI component of the fuel strip (if in UI).
    [SerializeField] private Image fuelStripImage;

    // The SpriteRenderer component of the fuel strip
    // (if attached to side of vehicle).
    [SerializeField] private SpriteRenderer fuelStripRenderer;

    [Header("------------- SCENE OBJECTS -------------", order = 10)] [Space(10, order = 11)]

    // The Main Camera (Cinemachine Virtual Camera).
    [SerializeField] private CinemachineVirtualCamera mainVirtualCamera;

    [Header("---------------- PREFABS ----------------", order = 12)] [Space(10, order = 13)]

    // A Text prefab game object that floats upward and flashes "OUT OF FUEL"
    [SerializeField]
    private GameObject outOfFuelPrefab;

    [Header("---------------- SCRIPTABLE OBJECTS ----------------", order = 14)] [Space(10, order = 15)]
    
    // Players X and Y coordinates will be store in
    // scriptable object so that everyone can access them.
    [SerializeField]
    private FloatData playerXLocationObj;
    
    [SerializeField]
    private FloatData playerYLocationObj;
    
    [Header("---------------- EVENTS ----------------", order = 16)] [Space(10, order = 17)]
    
    // The Scriptable Object Game Event letting the
    // Scene Loader know to restart the scene
    [SerializeField]
    private VoidEvent restartLevelEvent;

    //-----------------------------------------------------------------

    //----------- Private Variables (hidden from inspector) -----------

    // Array of every car in the scene.
    private GameObject[] _allCars;

    // List of every "TruckControls" script in the scene.
    private List<TruckControls> _allCarsScripts = new List<TruckControls>();

    // The Default amount (in seconds) of a full tank of fuel.
    private float _defaultFuelAmount;

    // The Scale of the fuel meter strip when it is full.
    private Vector3 _defaultFuelScale;
    
    // The Default color of a full tank of fuel.
    private Color _fullTankColor;

    // The current velocity average of the car.
    private float _averageVelocity = 0.0f;

    // Are we accelerating or not?
    private bool _accelerating = false;

    // Are we trying to tilt or not?
    private bool _tilting = false;

    // Our Rigidbody2D Component.
    private Rigidbody2D myRigidbody2D;

    // Boolean telling when we are out of fuel.
    private bool _fuelIsEmpty;
    
    // New WaitForSeconds Object with 1 Second inside.
    private WaitForSeconds _wfsObjOneSec = new WaitForSeconds(1.0f);

    //-----------------------------------------------------

    //-------------- The Start Method -------------------
    // This Method is called before the first frame update
    // (or at the gameObject's creation/reactivation). It
    // is mainly used for variable setup.
    //-----------------------------------------------------
    void Start()
    {
        // Get our Rigidbody2D Component.
        myRigidbody2D = GetComponent<Rigidbody2D>();
        // Get all the cars in the scene with the "Vehicle" tag.
        _allCars = GameObject.FindGameObjectsWithTag("Vehicle");
        // Get their scripts as well
        foreach (GameObject car in _allCars)
        {
            // Add each of them to the full list
            _allCarsScripts.Add(car.GetComponent<TruckControls>());
        }

        // Double check that we collected the cars properly.
        // If not, print an error and disable the script.
        if (_allCarsScripts.Count == 0)
        {
            Debug.Log("Error: Scene is missing cars with 'Vehicle' " +
                      "tag and 'TruckControls.cs' Script");
            this.enabled = false;
        }

        if (secondsOfFuel != 0)
        {
            // This if statement deals with either the case of
            // the Fuel meter being a UI Image in the corner, or
            // being attached to the side of the vehicle.
            // (different vehicle prefabs display it differently)
            if (fuelMeterIsAttached)
            {
                // If the fuel is not empty, save it as
                // the default (full) fuel meter color.
                _fullTankColor = fuelStripRenderer.color;
            }
            else
            {
                _fullTankColor = fuelStripImage.color;
            }
            // Save the default scale of the fuel strip meter
            _defaultFuelScale = fuelColorStrip.localScale;
            // Also save its fuel amount as the default value.
            _defaultFuelAmount = secondsOfFuel;
        }
    }

    //------- The Update Method ------
    // This Method is called once per frame,
    // and is somewhat similar to FixedUpdate.
    // Here, it is used for things that FixedUpdate
    // does not need to do (e.g. user inputs).
    //--------------------------------------
    void Update()
    {
        // Let everyone in the scene know where the (active)
        // player is through these scriptable objects.
        var position = transform.position;
        playerXLocationObj.value = position.x;
        playerYLocationObj.value = position.y;
        if (Input.GetButton("Vertical"))
        {
            // If button held down, let FixedUpdate
            // know to start spinning the wheels
            // through this variable.
            _accelerating = true;
            // if we are pressing forward
            if (Input.GetAxis("Vertical") > 0.0f)
            {
                // Drain the vehicle's fuel tank
                DrainFuel();
            }
        }
        else if (_accelerating)
        {
            // If not pressed, let FixedUpdate
            // know through this variable.
            _accelerating = false;
        }

        if (Input.GetButton("Horizontal"))
        {
            // If button held down, let FixedUpdate know
            // to start tilting through this variable.
            _tilting = true;
        }
        else if (_tilting)
        {
            // If not pressed, let FixedUpdate
            // know through this variable.
            _tilting = false;
        }

        // If we are accelerating.
        if (_accelerating)
        {
            if (accelerationAudio.volume < 1.0f)
            {
                // Turn up the acceleration audio (if not already turned up).
                accelerationAudio.volume += Time.deltaTime;
            }
        }
        // If not accelerating
        else
        {
            if (accelerationAudio.volume > 0.0f)
            {
                // Turn down the acceleration audio (if volume not at zero).
                accelerationAudio.volume -= Time.deltaTime;
            }
        }
    }

    //------- The FixedUpdate Method ------
    // This Method is called a fixed number
    // of times per second (e.g. 50 FPS).
    // It is generally used when applying
    // force to the physics engine (collision
    // calculations are finished), and is similar to
    // "Update." Here, we track the user input
    // and apply forces to the wheels and vehicle
    // tilt accordingly.
    //--------------------------------------
    void FixedUpdate()
    {
        // If the car was moving fast in the saved velocity (last frame),
        // but is now moving slow (this frame), that means an impact took place.
        // Play the impact audio.
        if ((math.abs(myRigidbody2D.velocity.x) +
             math.abs(myRigidbody2D.velocity.y)) / 2 <
            _averageVelocity - 1.0f)
        {
            // Double check that it's not already playing
            if (!impactAudio.isPlaying)
            {
                // Set it to a random pitch
                impactAudio.pitch = Random.Range(0.75f, 1.25f);
                impactAudio.Play();
            }
        }

        // Save the current (absolute) average
        // velocity of the vehicle
        _averageVelocity = (math.abs(myRigidbody2D.velocity.x) +
                            math.abs(myRigidbody2D.velocity.y)) / 2;
        // Check that the fuel's not empty.
        // If it is, end the Method.
        if (_fuelIsEmpty)
        {
            return;
        }

        // Check that the Update method is
        // telling us to spin the wheels.
        if (_accelerating)
        {
            // Set up the rotation variable in
            // preparation for applied angular force
            // to the wheels.
            // (Multiply Axis by sensitivity variable).
            float rotation = Input.GetAxis("Vertical") * -wheelSpeed;
            // Access each wheel in the wheels list
            foreach (Rigidbody2D wheel in wheels)
            {
                // Turn this wheel
                wheel.AddTorque(rotation);
                // To reduce glitches, we will
                // clamp (limit) the wheels'
                // positions and rotation speeds.
                // (prevents axel separation glitch)
                float maxDistance = 0.7f;
                float maxSpin = 10000.0f;
                // Limit position.
                wheel.transform.localPosition =
                    Vector3.ClampMagnitude(wheel.transform.localPosition,
                        maxDistance);
                // Limit spin.
                wheel.angularVelocity = Mathf.Clamp(wheel.angularVelocity, -maxSpin, maxSpin);
            }
        }

        // Check that the Update method
        // is telling us to tilt the vehicle.
        if (_tilting)
        {
            // Set up the rotation variable in
            // preparation for applied angular force
            // (Multiply Axis by sensitivity variable).
            float rotation = Input.GetAxis("Horizontal") * -tiltSensitivity;
            // Turn (tilt) the whole truck
            myRigidbody2D.AddTorque(rotation);
            // Limit our angular (spinning) velocity so it
            // doesn't get too fast (reduces glitches).
            float maxSpinSpeed = 350.0f;
            myRigidbody2D.angularVelocity = Mathf.Clamp(myRigidbody2D.angularVelocity, -maxSpinSpeed, maxSpinSpeed);
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
        Vector3 fuelStripScale = fuelColorStrip.localScale;

        // Shrink the fuel strip on the x axis
        // until it reaches (almost) zero.
        if (fuelColorStrip.localScale.x > 0.01)
        {
            // Calculate the shrink speed based on the scale remaining, and 
            // amount of fuel time left.
            float shrinkSpeed = (fuelColorStrip.localScale.x / secondsOfFuel);
            // Shrink on the X Axis (if greater than 0).
            if (fuelStripScale.x > 0)
            {
                // Shrink on X
                fuelStripScale.x -= shrinkSpeed * Time.deltaTime;
            }
            else if (fuelStripScale.x < 0)
            {
                // If X scale passes zero, set it back to zero.
                fuelStripScale.x = 0;
            }

            // Get the color.
            // (from either the UI or the vehicle's side indicator)
            Color spriteColor;
            if (fuelMeterIsAttached)
            {
                spriteColor = fuelStripRenderer.color;
            }
            else
            {
                spriteColor = fuelStripImage.color;
            }

            // Manipulate the color over time from green to red (using RGB).
            spriteColor.g -= (spriteColor.g / secondsOfFuel) * Time.deltaTime;
            spriteColor.b -= (spriteColor.b / secondsOfFuel) * Time.deltaTime;
            spriteColor.r += (spriteColor.r / secondsOfFuel) * Time.deltaTime * 5;
            // Reassign the color.
            if (fuelMeterIsAttached)
            {
                fuelStripRenderer.color = spriteColor;
            }
            else
            {
                fuelStripImage.color = spriteColor;
            }

        }
        else if (fuelStripScale.x < 0.01)
        {
            // If the scale passes 0.01,
            // reset it to zero (x axis)
            // and end the script.
            fuelStripScale.x = 0.0f;
        }

        // Reassign the fuel strip's scale
        fuelColorStrip.localScale = fuelStripScale;
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
        // Create "Out Of Fuel" flashing message.
        Instantiate(outOfFuelPrefab, transform.position, quaternion.identity);
        // Add Drag to our rigidbody.
        myRigidbody2D.drag = 1;
        myRigidbody2D.angularDrag = 0.5f;
        // Wait a second to switch vehicles.
        yield return _wfsObjOneSec;
        bool nextCarFound = false;
        // Look through all cars in the scene
        foreach (var carScript in _allCarsScripts)
        {
            // Check if they are the next vehicle based on their ID.
            if (carScript.carImportance == carImportance + 1)
            {
                // Enabled their controls (script).
                carScript.enabled = true;
                // Set the virtual camera to follow them.
                mainVirtualCamera.Follow = carScript.transform;
                // More cars exist. We don't need to restart.
                nextCarFound = true;
            }
        }
        // We don't have any more cars left.
        // Let the level Loader know to reset the scene.
        if (!nextCarFound)
        {
            //   restartLevelEvent.Raise();
        }
        // Give the car a second to settle.
        yield return _wfsObjOneSec;
        // while loop continuously checks for car movement.
        while (!myRigidbody2D.isKinematic)
        {
            // Check that we are not moving (almost),
            // and still out of fuel.
            if (myRigidbody2D.velocity.x < 0.005f &&
                myRigidbody2D.velocity.y < 0.005f &&
                myRigidbody2D.angularVelocity < 0.005f &&
                _fuelIsEmpty)
            {
                // "isKinematic" will freeze the car's rigidbody.
                myRigidbody2D.isKinematic = true;
                // Add constraints just in case
                myRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
                // Loop twice for both wheels
                for (int i = 0; i < 2; i++)
                {
                    // Freeze the wheels as well.
                    wheels[i].isKinematic = true;
                    // And freeze the wheel's axel (its parent).
                    wheelAxles[i].isKinematic = true;
                }

                // Leave the while loop.
                break;
            }
            else
            {
                // If it's still moving (and the fuel's off),
                // wait another second (while loop resets).
                yield return _wfsObjOneSec;
            }
        }

        if (secondsOfFuel > 0.0f)
        {
            // If the fuels was turned back on while
            // we were doing this, stop this method.
            _fuelIsEmpty = false;
            yield return false;
        }
        else
        {
            // If the acceleration audio is still on at this point
            // make sure we turn it off before disabling this script.
            while (accelerationAudio.volume > 0.0f)
            {
                accelerationAudio.volume -= Time.deltaTime;
                // Wait one frame. Then continue the loop.
                yield return 1;
            }
        }
    }

    //------- The RestoreFuel Method ------------
    // This Method is called by the "Fuel Item
    // Pickup" object when the car triggers its
    // collider. This Method deals with restoring
    // the fuel amount, fuel meter, and rigidbodies
    // to their default state.
    //----------------------------------------
    public void RestoreFuel(bool superFuel)
    {
        // If the out of fuel coroutine is still going, we need to stop it.
        StopCoroutine(OutOfFuel());
        // Let the other methods know the fuel is back with this variable.
        _fuelIsEmpty = false;
        // Get rid of drag for our rigidbody.
        myRigidbody2D.drag = 0;
        myRigidbody2D.angularDrag = 0;
        // Reactivate the truck's rigidbody
        myRigidbody2D.constraints = RigidbodyConstraints2D.None;
        myRigidbody2D.isKinematic = false;
        // Restore the fuel (in seconds) to the full default value.
        secondsOfFuel = _defaultFuelAmount;
        // Reset the color of the fuel meter (UI or side indicator).
        if (fuelMeterIsAttached)
        {
            fuelStripRenderer.color = _fullTankColor;
        }
        else
        {
            fuelStripImage.color = _fullTankColor;
        }

        // Reset the size of the fuel meter (mainly on the x axis).
        fuelColorStrip.localScale = _defaultFuelScale;
        for (int i = 0; i < 2; i++)
        {
            // Unfreeze the wheels as well.
            wheels[i].isKinematic = false;
            // And Unfreeze the wheel's axel (its parent).
            wheelAxles[i].isKinematic = false;
        }

        // If we picked up "super fuel," initiate a few second speed boost. 
        if (superFuel)
        {
            StartCoroutine(SpeedBoost());
        }
    }

    //----- The SpeedBoost Coroutine --------
    // This Coroutine sets the vehicle's (wheel) speed
    // to a much higher value, but only for a few seconds.
    // This Coroutine is called when the player vehicle
    // collects a "Super Fuel" pickup.
    //-------------------------------------------------
    IEnumerator SpeedBoost()
    {
        // Save the old wheel material's friction.
        float originalFriction = wheels[0].sharedMaterial.friction;
        // Make the wheels slippery.
        wheels[0].sharedMaterial.friction = 0;
        wheels[1].sharedMaterial.friction = 0;
        // Push the car forward (right direction from our POV).
        myRigidbody2D.AddForce(transform.right * 3000);
        // This will help to stabilize the rotation.
        myRigidbody2D.AddTorque(boostAngularForce);
        // We will save the size of the blue
        // trail particle in this variable.
        Vector3 currentScale;
        // Gradually reset the friction over time until
        // we get back to the original friction value.
        while (wheels[0].sharedMaterial.friction < originalFriction)
        {
            // Add time to the friction
            wheels[0].sharedMaterial.friction += Time.deltaTime * 2;
            wheels[1].sharedMaterial.friction += Time.deltaTime * 2;
            // Scale up the blue speed trail particle
            // transform on the x axis over time
            // (while it is less than one).
            if (yellowTrailParticle.localScale.x < 1.0f)
            {
                // Grab the scale
                currentScale = yellowTrailParticle.localScale;
                currentScale.x += Time.deltaTime;
                yellowTrailParticle.localScale = currentScale;
            }

            // Wait one frame, and come back.
            yield return 0;
        }

        // Double check that the friction reset exactly.
        wheels[0].sharedMaterial.friction = originalFriction;
        wheels[1].sharedMaterial.friction = originalFriction;
        // Scale down the blue trail over time (X axis)
        while (yellowTrailParticle.localScale.x > 0.0f)
        {
            // Get the scale
            currentScale = yellowTrailParticle.localScale;
            // Shrink the X axis by time
            currentScale.x -= Time.deltaTime / 2;
            // Reassign the scale
            yellowTrailParticle.localScale = currentScale;
            // Wait one frame, and come back.
            yield return 0;
        }

        // Double check that X axis resets to 0.
        currentScale = yellowTrailParticle.localScale;
        currentScale.x = 0;
        yellowTrailParticle.localScale = currentScale;
    }
}

// ---------------------- END OF FILE -----------------------