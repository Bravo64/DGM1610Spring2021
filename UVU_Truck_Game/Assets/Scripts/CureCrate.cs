using System;
using System.Collections;
using System.Collections.Generic;
using GameEvents;
using UnityEngine;

public class CureCrate : MonoBehaviour
{
    /*
---------------- Documentation ---------------------

Script's Name: CureCrate.cs
Author: Keali'i Transfield

Script's Description: This script is for the cure crates being 
held in the back of the players truck.
    
Script's Methods:
    - Start
    - OnTriggerEnter2D
    - OnTriggerExit2D
    - OnCollisionEnter2D

--------------------- DOC END ----------------------
 */
    
    [Header("------------ SCRIPTABLE OBJECTS ------------", order = 0)] [Space(10, order = 1)]
    
    // The integer type Scriptable Object
    // where the number of crates in the
    // cargo is being saved.
    [SerializeField]
    private IntData carriedCrates;
    // The integer type Scriptable Object
    // where the number of crates that have not
    // touched the ground and died, is being saved.
    [SerializeField]
    private IntData livingCrates;

    [Header("---------------- EVENTS ----------------", order = 2)] [Space(10, order = 3)]
    
    // The Scriptable Object Game Event letting the
    // Text Updater to update the Cure Crate UI Text.
    [SerializeField]
    private VoidEvent updateCrateText;

    [Header("---------------- CHILDREN ----------------", order = 4)] [Space(10, order = 5)]
    
    // The bright blue inner sprite.
    [SerializeField]
    private GameObject blueInside;
    // The bright blue particle trail object.
    [SerializeField]
    private GameObject blueTrail;
    // The particle object with the smash effect (and audio)
    [SerializeField]
    private GameObject smashParticleObject;
    private bool _dead = false;
    
    
    //-------------- The Start Method -------------------
    // This Method is called before the first frame update
    // (or at the gameObject's creation/reactivation). It
    // is mainly used for variable setup.
    //-----------------------------------------------------
    private void Start()
    {
        // We are alive, so add one to the living counter.
        livingCrates.value += 1;
    }

    //------- The OnTriggerEnter2D Method ----------
    // This Method is called when this object
    // enters a trigger collider (i.e. the cargo
    // collider of the truck). It then updates the
    // crate values and raises a game event so the
    // text updater knows.
    //------------------------------------------
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Don't do anything if we are dead.
        if (_dead)
        {
            return;
        }
        
        // Check for the tag of the back of the truck.
        if (other.CompareTag("Cargo"))
        {
            carriedCrates.value += 1;
            updateCrateText.Raise();
        }
    }

    //------- The OnTriggerEnter2D Method --------
    // This Method is called when this object
    // leaves a trigger collider (i.e. the cargo
    // collider of the truck). It then updates the
    // crate values (with subtraction) and raises
    // a game event so the text updater knows.
    //------------------------------------------
    private void OnTriggerExit2D(Collider2D other)
    {
        // Don't do anything if we are dead.
        if (_dead)
        {
            return;
        }

        // Check for the tag of the back of the truck.
        if (other.CompareTag("Cargo"))
        {
            carriedCrates.value -= 1;
            updateCrateText.Raise();
        }
    }

    //------- The OnCollisionEnter2D Method --------
    // This Method is called when this object
    // touches another collision object. In this case,
    // it is used for when we touch the ground and die.
    //------------------------------------------
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Don't do anything if we are dead.
        if (_dead)
        {
            return;
        }

        // Check that we hit the ground by the tag
        if (other.gameObject.CompareTag("Ground"))
        {
            // Update the Scriptable Object value
            livingCrates.value -= 1;
            // Change our tag so we also
            // kill others that touch us.
            gameObject.tag = "Ground";
            // Turn off the blue stuff
            blueInside.SetActive(false);
            blueTrail.SetActive(false);
            // Turn on the smashing effect (with audio)
            smashParticleObject.SetActive(true);
            // Turn everything off.
            _dead = true;
            this.enabled = false;
        }
        
        // Double check that nothing was calculated improperly.
        // If the number of crates still alive falls below the
        // number of crates in the cargo, we need to fix that.
        if (livingCrates.value < carriedCrates.value)
        {
            // Update the Scriptable Object value
            carriedCrates.value = livingCrates.value;
            // Let the Text Updater know
            updateCrateText.Raise();
        }
    }
}

// ---------------------- END OF FILE -----------------------
