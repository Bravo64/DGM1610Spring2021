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
        
    REQUIREMENTS:
        - "Coin_Particle" Game Object
            (Folder Path: /Resources/Prefabs/Coin_Particle)

    --------------------- DOC END ----------------------
     */
    
    // The particle effect we want to create when fuels is collected (prefabs found in Resources)
    private GameObject _particlePrefab;
    
    // The value of the coin for the player score
    private int coinValue = 150;
    
    //-------------- The Start Method -------------------
    // This Method is called before the first frame update
    // (or at the gameObject's creation/reactivation).
    // Here, we pretty much just use it to assign
    // the Particle Prefab.
    //-----------------------------------------------------

    void Start()
    {
        // Get the Particle Prefab from Resources folder.
        _particlePrefab = Resources.Load("Prefabs/Coin_Particle") as GameObject;
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
            LevelManager._instance.AddToScore(coinValue);
            
            if (_particlePrefab)
            {
                // Create the fuel particle effect.
                Instantiate(_particlePrefab, transform.position, Quaternion.identity);
            }
            else
            {
                // If we can't find the particle, print an error message.
                Debug.LogError("'Coin_Particle' Prefab is missing (Location: /Assets/Resources/Prefabs/Coin_Particle').'");
            }
                // Turn off this object.
            gameObject.SetActive(false);
        }
    }
}
// -------------------- END OF FILE -----------------------
