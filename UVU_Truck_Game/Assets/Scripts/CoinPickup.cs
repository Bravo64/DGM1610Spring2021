using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{ 
    /*
    ---------------- Documentation ---------------------

    Script's Name: FuelItemPickup.cs
    Author: Keali'i Transfield

    Script's Description: This script is for a coin pickup object. 
        This script waits for a "Vehicle" tagged object to enter 
        and trigger this object's trigger collider. It then calls
        the static level manager and activates its "AddCoin" method.
        
    Script's Methods:
        - Start
        - OnTriggerEnter2D

    --------------------- DOC END ----------------------
     */
    
    [Header("---------------- PREFABS ----------------", order = 0)]
    [Space(10, order = 1)]
    
    // The particle effect we want to create when fuels is collected
    [SerializeField]
    private GameObject coinParticle;
    
    // The Level Manager in the scene
    private LevelManager _levelManager;

    // The value of the coin for the player score
    private int coinValue = 123;
    
    //-------------- The Start Method -------------------
    // This Method is called before the first frame update
    // (or at the gameObject's creation/reactivation).
    // Here, we pretty much just use it to assign
    // the Level Manager.
    //-----------------------------------------------------

    void Start()
    {
        // Grab the Level Manager in the scene
        _levelManager = Transform.FindObjectOfType<LevelManager>();
    }

    //------- The OnTriggerEnter2D Method ------------
    // This Method is called once another object enters
    // and this triggers this object's trigger collider.
    // In this case, this object checks that the object
    // is a vehicle, and acts accordingly (adding coin
    // score and disabling itself).
    //--------------------------------------
    void OnTriggerEnter2D(Collider2D col)
    {
        // Check for "Vehicle" tag
        if (col.CompareTag("Vehicle"))
        {
            // Tell the static Level Manager to
            // add the coin value to the score.
            _levelManager.AddToScore(coinValue);
            
            // Create the fuel particle effect.
            Instantiate(coinParticle, transform.position, Quaternion.identity);

            // Turn off this object.
            gameObject.SetActive(false);
        }
    }
}
// -------------------- END OF FILE -----------------------
