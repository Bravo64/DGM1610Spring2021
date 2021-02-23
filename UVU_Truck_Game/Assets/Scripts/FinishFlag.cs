using System;
using System.Collections;
using System.Collections.Generic;
using GameEvents;
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
        - OnTriggerEnter

    --------------------- DOC END ----------------------
    */
    
    [Header("------------- EVENTS -------------", order = 0)]
    [Space(10, order = 1)]
    
    // The Game Event that broadcasts that the level was completed
    [SerializeField]
    private VoidEvent levelCompletedEvent;

    [Header("--------------- PREFABS ---------------", order = 2)]
    [Space(10, order = 3)]
    
    // The flag particle child object (with the audio as well)
    [SerializeField]
    private GameObject flagParticle;

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
            // Turn on the level complete particle
            flagParticle.SetActive(true);
            // Televise that the level has been completed.
            levelCompletedEvent.Raise();
        }
    }
    
}
// ---------------------- END OF FILE -----------------------
