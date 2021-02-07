using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishFlag : MonoBehaviour
{
    /*
    ---------------- Documentation ---------------------

    Script's Name: FinishFlag.cs
    Author: Keali'i Transfield

    Script's Description: This script is for the flag at the finish line
        of the level. The flag waits until an object with a "Vehicle" tag
        triggers its trigger collider component. It then plays a level
        complete sounds and tells the Level Manager that the level 
        is completed.
        
    Script's Methods:
        - Start
        - OnTriggerEnter
        
    REQUIREMENTS:
        - "Flag_Particle" child Game Object

    --------------------- DOC END ----------------------
    */

    // The flag particle child object (with the audio as well)
    private GameObject _flagParticle;
    
    //-------------- The Start Method -------------------
    // This Method is called before the first frame update
    // (or at the gameObject's creation/reactivation). Here,
    // it is mainly used for getting and setting up variables
    //-----------------------------------------------------
    private void Start()
    {
        // Cycle through our children
        foreach (Transform child in transform)
        {
            // Check for the flag particle child object's name
            if (child.name == "Flag_Particle")
            {
                // Save the child
                _flagParticle = child.gameObject;
            }
        }
        // Double check that we got it
        if (!_flagParticle)
        {
            // If not, print an error and disable the script
            Debug.LogError("Error: The Finish Line Flag's 'Flag_Particle' child is " +
                           "missing. (Location in Scene: /Flag_Finish/Flag_Particle)");
            // Disable this script
            this.enabled = false;
        }
    }
    
    //------- The OnTriggerEnter2D Method ------------
    // This Method is called once another object enters
    // and triggers this object's trigger collider.
    // In this case, this finish mark flag checks that
    // the object is "Vehicle" tagged, and then initiates
    // the level complete process.
    //--------------------------------------
    void OnTriggerEnter2D(Collider2D col)
    {
        // Check for "Vehicle" tag on other object.
        if (col.CompareTag("Vehicle"))
        {
            // Activate the particle
            // (with attached audio as well)
            _flagParticle.SetActive(true);
            // Let the Level Manager know
            // that we have completed the level.
            LevelManager._instance.LevelComplete();
        }
    }
    
}
