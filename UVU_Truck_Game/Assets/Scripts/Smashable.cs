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

    ----------------------------------------------------
     */
    
    [Header("---------------- VALUES ----------------", order = 0)] [Space(10, order = 1)]
    
    [UnityEngine.Range(0.0f, 3.0f)]
    [SerializeField] 
    private float minimumPitch = 0.65f;
    
    [UnityEngine.Range(0.0f, 3.0f)]
    [SerializeField] 
    private float maximumPitch = 3.0f;
    
    [Header("---------------- CHILDREN ----------------", order = 0)] [Space(10, order = 1)]
    
    // The child object holding the smashing particle component
    [SerializeField] 
    private GameObject explosionParticle;
    
    // The Game Object holding sprite decorations of the building
    [SerializeField] 
    private GameObject decorations;

    // The AudioSource Component on the Particle Object.
    [SerializeField]
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
    }

    //------- The Smash Method ------------
    // This Method is called once another object enters
    // and triggers this object's trigger collider.
    // In this case, this object checks that the object
    // is a vehicle, and acts accordingly (disabling itself
    // and enabling explosion particle).
    //------------------------------------------
    public void Smash()
    {
        // If we are dead, don't do anything
        if (_dead)
        {
            return;
        }
        // Turn on explosion particle child
        explosionParticle.SetActive(true);
        // Make it so it has no parent
        explosionParticle.transform.parent = null;
        if (transform.parent != null)
        {
            // If we have a parent, turn it off.
            transform.parent.gameObject.SetActive(false);
        }
        else
        {
            // If not, turn ourselves off.
            gameObject.SetActive(false);
        }
        // Set my audio to a random pitch.
        _myAudio.pitch = Random.Range(minimumPitch, maximumPitch);
        // Play my Audio if it's not already playing
        // (found on the child particle object).
        if (!_myAudio.isPlaying)
        {
            _myAudio.Play();
        }
        _dead = true;
        }
}

// ---------------------- END OF FILE -----------------------