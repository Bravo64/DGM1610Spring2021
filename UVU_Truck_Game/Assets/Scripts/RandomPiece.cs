using System;
using System.Collections;
using System.Collections.Generic;
using GameEvents;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomPiece : MonoBehaviour
{
    
    /*
    ---------------- Documentation ---------------------

    Script's Name: RandomPieceCreator.cs
    Author: Keali'i Transfield

    Script's Description: This script checks how far away 
        we are from the active player. If we are close enough, 
        it then instantiates a random piece at child end point's
        position, and then disables the script. If we are not 
        close enough, it waits a bit (coroutine), comes back, 
        and check the distance again (while loop set to true).
        
    Script's Methods:
        - Start (IEnumerator / Coroutine)

    --------------------- DOC END ----------------------
     */

    //---------- Public and Static Variables (visible in inspector)-----------

    [Header("------------ VALUE VARIABLES ------------", order = 0)] [Space(10, order = 1)]
    
    // These will determine the possible
    // sizes when we randomize a pieces scale
    [UnityEngine.Range(0.0f, 10.0f)]
    [SerializeField]
    private float minXScale = 0.5f;
    [UnityEngine.Range(0.0f, 10.0f)]
    [SerializeField]
    private float maxXScale = 5.0f;

    [Header("--------------- CHILDREN ---------------", order = 2)] [Space(10, order = 3)]
    
    // The end point of our object.
    // (where we will create the next piece)
    [SerializeField]
    private Transform endPoint;
    
    [Header("---------------- PREFABS ----------------", order = 4)] [Space(10, order = 5)]
    
    // An array of all the random pieces we have to choose from.
    [SerializeField]
    private GameObject[] randomPieces;

    [Header("-------------- SCRIPTABLE OBJECTS --------------", order = 6)]
    [Space(10, order = 7)]

    // The location of the player (on the X Axis)
    // is saved inside a FloatData Scriptable Object.
    [SerializeField]
    private FloatData playerXLocation;

    //-----------------------------------------------------------------
    
    //----------- Private Variables (Hidden from Inspector) -----------

    // The index of the random piece
    // we choose from the array.
    private int _chosenPiece;
    
    // How long we wait before checking
    // the player's distance again.
    // (so we're not needlessly
    // doing it all the time)
    private float _waitInterval;
    
    // The player GameObject in the scene.
    private GameObject _activePlayerVehicle;
    
    // Our WaitForSeconds Object that will
    // hold a random wait time.
    private WaitForSeconds _wfsObj;

    //-----------------------------------------------------------------
    
    
    //-------------- The Start Method -------------------
    // This Method is called before the first frame update
    // (or at the gameObject's creation/reactivation). It
    // is mainly used for game object setup and selecting
    // some random values. In this case, it also acts as a
    // coroutine and check the distance from the player
    // in intervals. When the player is close enough, we 
    // create the next piece.
    //-----------------------------------------------------
    private IEnumerator Start()
    {
        // Get our scale.
        Vector3 myScale = transform.localScale;
        // Randomize the x scale based on the size limit variables.
        myScale.x = Random.Range(minXScale, maxXScale);
        // Reassign our scale
        transform.localScale = myScale;
        // Choose a random piece index.
        _chosenPiece = Random.Range(0, randomPieces.Length);
        // Make a new WaitForSeconds Object
        // and Choose a random wait time.
        _wfsObj = new WaitForSeconds(Random.Range(0.75f, 1.0f));
        
        // Check our distance from the player inside this loop
        while (true)
        {
            // Check our distance from the player on the X axis
            // (Player X Location is a Scriptable Object)
            if (transform.position.x - playerXLocation.value < 100.0f)
            {
                // We are close enough to the player.
                // Create the next piece at our
                // end point's position.
                
                Instantiate(randomPieces[_chosenPiece], endPoint.position, endPoint.rotation);
                
                // Leave the loop
                break;
            }
            else
            {
                // If not close enough yet, wait a bit
                // and come back (loop continues)
                yield return _wfsObj;
            }
        }
        // We are done. Disable this script.
        this.enabled = false;
    }
}

// ---------------------- END OF FILE -----------------------
