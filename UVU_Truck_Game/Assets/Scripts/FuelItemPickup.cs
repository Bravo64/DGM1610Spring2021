﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelItemPickup : MonoBehaviour
{ 
    /*
---------------- Documentation ---------------------

Script's Name: FuelItemPickup.cs

Script's Description: This script waits for a "Vehicle" tagged object
    to enter and trigger this object's trigger collider. It then grabs
    the "TruckControls" scripts of the other object, and calls its
    RestoreFuel method. Lastly, the object disables itself.
    
Script's Methods:
    - OnTriggerEnter2D

--------------------- DOC END ----------------------
 */
    
    //------- The OnTriggerEnter2D Method ------------
    // This Method is called once another object enters
    // and this triggers this object's trigger collider.
    // In this case, this object checks that the object
    // is a vehicle, and acts accordingly (ending with
    // disabling itself).
    //--------------------------------------
    void OnTriggerEnter2D(Collider2D col)
    {
        // Check for "Vehicle" tag
        if (col.CompareTag("Vehicle"))
        {
            // Get the controls script from the vehicle
            TruckControls truckScript = col.GetComponent<TruckControls>();
            // Make sure the script is enabled
            truckScript.enabled = true;
            // Call its public RestoreFuel method,
            // so that it can work on reactivating itself.
            truckScript.RestoreFuel();
            // Turn off this object.
            gameObject.SetActive(false);
        }
    }
}
