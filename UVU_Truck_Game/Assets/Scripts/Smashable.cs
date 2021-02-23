using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Smashable : MonoBehaviour
{
    /*
    ---------------- Documentation ---------------------

    Script's Name: Smashable.cs
    Author: Keali'i Transfield

    Script's Description: This script checks for a collision with
    the player vehicle, and then deactivates its sprites
    and enables the destruction particle (both are children).
        
    Script's Methods:
        - OnTriggerEnter2D
        - Start

    --------------------- DOC END ----------------------
     */
    
    [Header("---------------- CHILDREN ----------------", order = 0)] [Space(10, order = 1)]
    
    // The child object holding the smashing particle component
    [SerializeField] 
    private GameObject explosionParticle;
    
    // The Game Object holding sprite decorations of the building
    [SerializeField] 
    private GameObject decorations;
    
    [Header("---------------- BOOLEANS ----------------", order = 0)] [Space(10, order = 1)]
    
    // Should we change the pitch when we play it
    [SerializeField]
    private bool changeAudioPitch = true;
    
    // My AudioSource Component.
    private AudioSource _myAudio;
    
    // Shows that we have collided with player
    private bool _dead = false;

    //-------------- The Start Method -------------------
    // This Method is called before the first frame update
    // (or at the gameObject's creation/reactivation). It
    // is mainly used for variable setup.
    //-----------------------------------------------------
    private void Start()
    {
        // Reset the scale and parent in case the parent changes.
        transform.parent = null;
        transform.localScale = Vector3.one;
        // Get my AudioSource Component.
        _myAudio = GetComponent<AudioSource>();
    }

    //------- The OnTriggerEnter2D Method ------------
    // This Method is called once another object enters
    // and triggers this object's trigger collider.
    // In this case, this object checks that the object
    // is a vehicle, and acts accordingly (disabling itself
    // and enabling explosion particle).
    //------------------------------------------
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If we are dead, don't do anything
        if (_dead)
        {
            return;
        }
        // Check for "Vehilcle" tag
        if (other.CompareTag("Vehicle"))
        {
            // Turn on explosion particle child
            explosionParticle.SetActive(true);
            // Turn off building sprites
            decorations.SetActive(false);
            if (changeAudioPitch)
            {
                // Set my audio to a random pitch.
                _myAudio.pitch = Random.Range(0.65f, 3.0f);
            }
            // Play my Audio if it's not already playing
            if (!_myAudio.isPlaying)
            {
                _myAudio.Play();
            }
            _dead = true;
        }
    }
}

// ---------------------- END OF FILE -----------------------