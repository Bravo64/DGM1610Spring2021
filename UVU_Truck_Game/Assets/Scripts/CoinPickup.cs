using System.Collections;
using System.Collections.Generic;
using GameEvents;
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
        - OnTriggerEnter2D

    --------------------- DOC END ----------------------
     */
    
    [Header("---------------- PREFABS ----------------", order = 0)]
    [Space(10, order = 1)]
    
    // The particle effect we want to create when fuels is collected
    [SerializeField]
    private GameObject coinParticle;

    [Header("------------- Events -------------", order = 2)]
    [Space(10, order = 3)]
    
    // The Game Event that broadcasts when we added to the score
    [SerializeField]
    private IntEvent addedToScore;
    
    // The value of the coin for the player score
    private int coinValue = 123;

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
            // Tell the scene Level Manager to
            // add the coin value to the score
            // (televised through this scriptable object event).
            addedToScore.Raise(coinValue);
            
            // Create the fuel particle effect.
            Instantiate(coinParticle, transform.position, Quaternion.identity);

            // Turn off this object.
            gameObject.SetActive(false);
        }
    }
}
// -------------------- END OF FILE -----------------------
