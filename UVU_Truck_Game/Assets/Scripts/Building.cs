using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    /*
    ---------------- Documentation ---------------------

    Script's Name: Building.cs
    Author: Keali'i Transfield

    Script's Description: This script checks for a collision with
    the player vehicle, and then deactivates its building sprites
    and enables the destruction particle (both are children).
        
    Script's Methods:
        - OnTriggerEnter2D

    --------------------- DOC END ----------------------
     */
    
    [Header("---------------- CHILDREN ----------------", order = 0)] [Space(10, order = 1)]
    
    // The child object holding the smashing particle component
    [SerializeField] 
    private GameObject explosionParticle;
    
    // The Game Object holding sprite decorations of the building
    [SerializeField] 
    private GameObject buildingDecorations;
    
    //------- The OnTriggerEnter2D Method ------------
    // This Method is called once another object enters
    // and triggers this object's trigger collider.
    // In this case, this object checks that the object
    // is a vehicle, and acts accordingly (disabling itself
    // and enabling explosion particle).
    //------------------------------------------
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check for "Vehilcle" tag
        if (other.CompareTag("Vehicle"))
        {
            // Turn on explosion particle child
            explosionParticle.SetActive(true);
            // Turn off building sprites
            buildingDecorations.SetActive(false);
        }
    }
}

// ---------------------- END OF FILE -----------------------