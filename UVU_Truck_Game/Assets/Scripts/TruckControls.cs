using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using NUnit.Framework;
using Unity.Mathematics;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TruckControls : MonoBehaviour
{
    /*
    ---------------- Documentation ---------------------
    
    Script's Name: TruckControls.cs
    Author: Keali'i Transfield
    
    Script's Description: This script deals with moving the
        truck based on user input, and draining the fuel 
        supply accordingly. It also freezes the truck's
        rigidbody, and disables this script when the
        fuel is empty (sending controls to the next car).
        
    Script's Methods:
        - Update
        - FixedUpdate
        - DrainFuel
        - OutOfFuel (Coroutine)
        - RestoreFuel (public)
        - SpeedBoost (Coroutine)
    
    --------------------- DOC END ----------------------
     */

    //---------- Public and Static Variables (visible in inspector)-----------
    
    [Header("----------- VALUE VARIABLES -----------", order = 0)]
    [Space(10, order = 1)]
    
    // Car ID Number (Determines car activation order, 1 is main)
    [UnityEngine.Range(30, 1)]
    public int carImportance = 1;
    // Move Sensitivity.
    [UnityEngine.Range(0, 50)]
    [SerializeField]
    private int wheelSpeed = 20;

    // Tilt Sensitivity.
    [UnityEngine.Range(0, 150)]
    [SerializeField]
    private int tiltSensitivity = 70;

    // Time left on fuel.
    [UnityEngine.Range(0.0f, 20.0f)]
    [SerializeField]
    private float secondsOfFuel = 1.8f;

    // Boolean telling when we are out of fuel.
    private bool _fuelIsEmpty;
    
    [Header("------------ MY COMPONENTS ------------", order = 2)]
    [Space(10, order = 3)]
    
    // The truck's Rigidbody2D Component.
    [SerializeField]
    private Rigidbody2D myRigidbody2D;

    [Header("---------------- CHILDREN ----------------", order = 0)]
    [Space(10, order = 1)]
    
    // The AudioSource component of the Game
    // Object that has the impact sound effect.
    [SerializeField]
    private AudioSource impactAudio;
    
    // The AudioSource component of the Game
    // Object that has the acceleration sound effect.
    [SerializeField]
    private AudioSource accelerationAudio;
    
    // The transform of the super speed blue trail particle object
    [SerializeField]
    private Transform _blueTrailParticle;
    
    // List for the wheel car axles (Rigidbody2D).
    [SerializeField]
    private Rigidbody2D[] wheelAxles;
    
    [Header("------------- GRANDCHILDREN --------------", order = 0)]
    [Space(10, order = 1)]
    
    // List for the wheels of the car (Rigidbody2D)
    [SerializeField]
    private Rigidbody2D[] wheels;
    
    // Color strip that tells the player
    // how much fuel they have left.
    [SerializeField]
    private Transform fuelColorStrip;

    // The Sprite Renderer of the fuel strip.
    [SerializeField]
    private SpriteRenderer fuelStripRenderer;
    
    [Header("------------- SCENE OBJECTS -------------", order = 0)]
    [Space(10, order = 1)]
    
    // The Main Camera (Cinemachine Virtual Camera).
    [SerializeField]
    private CinemachineVirtualCamera mainVirtualCamera;
    
    [Header("---------------- PREFABS ----------------", order = 0)]
    [Space(10, order = 1)]
    
    // A Text prefab game object that floats upward and flashes "OUT OF FUEL"
    [SerializeField]
    private GameObject outOfFuelPrefab;

    //-----------------------------------------------------------------
    
    //----------- Private Variables (Hidden from Inspector) -----------
    
    // Array of every car in the scene.
    private GameObject[] _allCars;
    
    // List of every "TruckControls" script in the scene.
    private List<TruckControls> _allCarsScripts  = new List<TruckControls>();

    // The Default amount (in seconds) of a full tank of fuel.
    private float _defaultFuelAmount;
    
    // The Default color of a full tank of fuel.
    private Color _fullTankColor;
    
    // The current velocity average of the car.
    private float _averageVelocity = 0.0f;
    
    // Are we accelerating or not?
    private bool _accelerating = false;
    
    // Are we trying to tilt or not?
    private bool _tilting = false;

    //-----------------------------------------------------

    //-------------- The Start Method -------------------
    // This Method is called before the first frame update
    // (or at the gameObject's creation/reactivation). It
    // is mainly used for variable setup.
    //-----------------------------------------------------
    void Start()
    {
        if (secondsOfFuel != 0)
        {
            // If the fuel is not empty, save it as
            // the default (full) fuel meter color.
            _fullTankColor = fuelStripRenderer.color;
            // Also save its fuel amount as the default value.
            _defaultFuelAmount = secondsOfFuel;
            
        }
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
            Debug.Log("Error: Scene is missing cars with 'Vehicle' tag and 'TruckControls.cs' Script");
            this.enabled = false;
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
        if (Input.GetButton("Vertical"))
        {
            // If button held down, let FixedUpdate
            // know to start spinning the wheels
            // through this variable.
            _accelerating = true;
            // Drain the vehicle's fuel tank
            DrainFuel();
        }
        else if (Input.GetButtonUp("Vertical"))
        {
            // If not pressed (button goes up),
            // let FixedUpdate know.
            _accelerating = false;
        }

        if (Input.GetButton("Horizontal"))
        {
            // If button held down, let FixedUpdate know
            // to start tilting through this variable.
            _tilting = true;
        }
        else if (Input.GetButtonUp("Horizontal"))
        {
            // If not pressed (button goes up),
            // let FixedUpdate know.
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
            Color spriteColor = fuelStripRenderer.color;
            // Manipulate the color over time from green to red (using RGB).
            spriteColor.g -= (spriteColor.g / secondsOfFuel) * Time.deltaTime;
            spriteColor.b -= (spriteColor.b / secondsOfFuel) * Time.deltaTime;
            spriteColor.r += (spriteColor.r / secondsOfFuel) * Time.deltaTime * 5;
            // Reassign the color.
            fuelStripRenderer.color = spriteColor;
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
        myRigidbody2D.drag = 2;
        myRigidbody2D.angularDrag = 2;
        // Wait a second to switch vehicles.
        yield return new WaitForSeconds(1.0f);
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
            }
        }

        // Give the car a few seconds to settle.
        yield return new WaitForSeconds(1.5f);
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
                yield return new WaitForSeconds(1.0f);
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
        // Reactivate the trucks rigidbody
        myRigidbody2D.constraints = RigidbodyConstraints2D.None;
        myRigidbody2D.isKinematic = false;
        // Restore the fuel (in seconds) to the full default value.
        secondsOfFuel = _defaultFuelAmount;
        // Reset the color of the fuel meter.
        fuelStripRenderer.color = _fullTankColor;
        // Reset the size of the fuel meter (on x axis).
        Vector3 fuelMeterScale = fuelColorStrip.localScale;
        fuelMeterScale.x = 1.0f;
        fuelColorStrip.localScale = fuelMeterScale;
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
        // This will help to stabilize the rotation
        myRigidbody2D.AddTorque(5000);
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
            if (_blueTrailParticle.localScale.x < 1.0f)
            {
                // Grab the scale
                currentScale = _blueTrailParticle.localScale;
                currentScale.x += Time.deltaTime;
                _blueTrailParticle.localScale = currentScale;
            }

            // Wait one frame, and come back.
            yield return 0;
        }

        // Double check that the friction reset exactly.
        wheels[0].sharedMaterial.friction = originalFriction;
        wheels[1].sharedMaterial.friction = originalFriction;
        // Scale down the blue trail over time (X axis)
        while (_blueTrailParticle.localScale.x > 0.0f)
        {
            // Get the scale
            currentScale = _blueTrailParticle.localScale;
            // Shrink the X axis by time
            currentScale.x -= Time.deltaTime / 2;
            // Reassign the scale
            _blueTrailParticle.localScale = currentScale;
            // Wait one frame, and come back.
            yield return 0;
        }

        // Double check that X axis resets to 0.
        currentScale = _blueTrailParticle.localScale;
        currentScale.x = 0;
        _blueTrailParticle.localScale = currentScale;
    }
}
// ---------------------- END OF FILE -----------------------