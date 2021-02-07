using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelItemPickup : MonoBehaviour
{ 
    /*
    ---------------- Documentation ---------------------

    Script's Name: FuelItemPickup.cs
    Author: Keali'i Transfield

    Script's Description: This script waits for a "Vehicle" tagged object
        to enter and trigger this object's trigger collider. It then grabs
        the "TruckControls" scripts of the other object, and calls its
        RestoreFuel method. Lastly, the object disables itself.
        
    Script's Methods:
        - Start
        - OnTriggerEnter2D

    REQUIREMENTS:
        - "Fuel_Particle" child Game Object
        (Folder Path: /Resources/Prefabs/Fuel_Particle)

    --------------------- DOC END ----------------------
     */
    
    // The particle effect we want to create when fuels is collected (prefabs found in Resources)
    private GameObject _particlePrefab;
    
    //-------------- The Start Method -------------------
    // This Method is called before the first frame update
    // (or at the gameObject's creation/reactivation).
    // Here, we pretty much just use it to assign
    // the Particle Prefab.
    //-----------------------------------------------------

    void Start()
    {
        // Get the Particle Prefab from Resources folder.
        _particlePrefab = Resources.Load("Prefabs/Fuel_Particle") as GameObject;
    }

    //------- The OnTriggerEnter2D Method ------------
    // This Method is called once another object enters
    // and triggers this object's trigger collider.
    // In this case, this object checks that the object
    // is a vehicle, and acts accordingly (ending with
    // disabling itself).
    //--------------------------------------
    void OnTriggerEnter2D(Collider2D col)
    {
        // Check for "Vehicle" tag on other object.
        if (col.CompareTag("Vehicle"))
        {
            // Get the controls script from the vehicle
            TruckControls truckScript = col.GetComponent<TruckControls>();
            // Make sure the script is enabled
            truckScript.enabled = true;
            // Call its public RestoreFuel method,
            // so that it can work on reactivating itself.
            truckScript.RestoreFuel();
            if (_particlePrefab)
            {
                // Create the fuel particle effect
                Instantiate(_particlePrefab, transform.position, Quaternion.identity);
            }
            else
            {
                // If we can't find the particle, print an error message.
                Debug.LogError("'Fuel_Particle' Prefab is missing (Location: /Assets/Resources/Prefabs/Fuel_Particle).'");
            }
                // Turn off this object.
            gameObject.SetActive(false);
        }
    }
}
// -------------------- END OF FILE -----------------------
