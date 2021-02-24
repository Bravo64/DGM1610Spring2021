using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieMover : MonoBehaviour
{
    /*
    ---------------- Documentation ---------------------

    Script's Name: ZombieMover.cs
    Author: Keali'i Transfield

    Script's Description: This script deals with moving the
        ZOMBIE item over time along a straight line to each 
        waypoint. A coroutine with random some pauses, as 
        well as some random directions changes will ensure 
        that the zombie has a bit of personality. The method 
        it uses is "MoveTowards" with a set of waypoint children.
        
    Script's Methods:
        - Start
        - Update

    --------------------- DOC END ----------------------
     */


    //----- Public and Serialized Variables (visable in editor) ------

    [Header("------------- VALUE VARIABLES -------------", order = 0)]
    [Space(10, order = 1)]
    
    [UnityEngine.Range(0.0f, 50.0f)]
    [SerializeField] 
    private float minMovementSpeed = 0.0f;
    
    [UnityEngine.Range(0.0f, 50.0f)]
    [SerializeField] 
    private float maxMovementSpeed = 7.5f;

    [UnityEngine.Range(0.0f, 20.0f)]
    [SerializeField] 
    private float minRandomWait = 1.0f;
    
    [UnityEngine.Range(0.0f, 20.0f)]
    [SerializeField] 
    private float maxRandomWait = 4.0f;
    
    [Header("--------------- SIBLINGS ---------------", order = 2)]
    [Space(10, order = 3)]
    
    // The zombie we are moving.
    [SerializeField]
    private Transform zombie;

    //------------------------------------------------------------


    //---------------- Private Variables -------------------

    // The random speed the zombie is moving at right now
    private float _currentMovementSpeed;
    
    // A list of (active) empties representing
    // each position we want to go to.
    private List<Transform> _waypoints = new List<Transform>();

    // A list of empties representing
    // each position we want to go to.
    private int _activeWaypoint = 0;

    // The direction we are moving in the waypoints,
    // which is kept track of with either a 1 or -1.
    private int _movementDirection = 1;
    
    // The Zombie will wait a randomly selected amount before
    // changing direction or pausing. These variables will keep
    // track of that.
    private float _randomWait1;
    private float _randomWait2;
    
    // The position the zombie was a last frame
    private Vector2 _positionLastFrame;

    //------------------------------------------------------


    //-------------- The Start Method -------------------
    // This Method is called before the first frame update
    // (or at the gameObject's creation/reactivation). It
    // is mainly used for setup and collecting the items
    // we need.
    //----------------------------------------------------
    void Start()
    {
        // Set our grandparent to null so our scale is not affected
        transform.parent.parent = null;
        // Pick a random speed for the zombie.
        _currentMovementSpeed = Random.Range(minMovementSpeed, maxMovementSpeed);
        // Get the active waypoint children.
        foreach (Transform child2 in transform)
        {
            // Add it to the list if it is active.
            if (child2.gameObject.activeSelf)
            {
                _waypoints.Add(child2);
            }
        }

        // Double check that we got the waypoints.
        if (_waypoints.Count == 0)
        {
            // If not, print and error message and disable this script.
            Debug.LogError("Error: Mover has no (active) waypoint children.");
            this.enabled = false;
        }
        // Select a new random amount of
        // time for our timer variables.
        _randomWait1 = Random.Range(minRandomWait, maxRandomWait);
        _randomWait2 = Random.Range(minRandomWait, maxRandomWait);
        // Set this variable so it's not at zero.
        _positionLastFrame = zombie.position;
        // Start the movement coroutine.
        StartCoroutine(MoveZombie());
    }

    //----- The MoveZombie Coroutine ---------
    // This Coroutine deals with moving the
    // zombie a little bit each frame. A while
    // loop set to true is used to ensure that
    // the coroutine is continuous. A coroutine
    // is used to add random pauses to the movement.
    // Random direction changes are added to give
    // the zombie some personality.
    //---------------------------------------
    IEnumerator MoveZombie()
    {
        // Loop every frame for as long as we need
        // (similar to the Update method).
        while (true)
        {
            // Move the zombie towards the active waypoint.
            zombie.position = Vector3.MoveTowards(zombie.position, _waypoints[_activeWaypoint].position,
                _currentMovementSpeed * Time.deltaTime);
            if (Vector3.Distance(zombie.position, _waypoints[_activeWaypoint].position) < 0.01f)
            {
                // If we reach the end.
                if (_activeWaypoint >= _waypoints.Count - 1)
                {
                    // Make sure we didn't go past.
                    _activeWaypoint = _waypoints.Count - 1;
                    // Go backwards
                    _movementDirection = -1;
                }
                // Else if we reach the beginning.
                else if (_activeWaypoint <= 0)
                {
                    // Make sure we're currently at zero.
                    _activeWaypoint = 0;
                    // Switch to forward.
                    _movementDirection = 1;
                }

                // Change to the next waypoint (index).
                // "_movementDirection" is either 1 or -1.
                _activeWaypoint += _movementDirection;
            }
            // Run down the timer if it's not at zero.
            if (_randomWait1 > 0)
            {
                _randomWait1 -= Time.deltaTime;
            }
            else
            {
                // Select a new random amount of time.
                _randomWait1 = Random.Range(minRandomWait, maxRandomWait);
                // Flip our movement direction.
                _movementDirection *= -1;

            }
            // Run down the timer if it's not at zero.
            if (_randomWait2 > 0)
            {
                _randomWait2 -= Time.deltaTime;
            }
            else
            {
                // Select a new random amount of time.
                _randomWait2 = Random.Range(minRandomWait, maxRandomWait);
                // Select a random time to wait.
                float pauseLength = Random.Range(minRandomWait/2, maxRandomWait);
                // Pause for a bit.
                yield return new WaitForSeconds(pauseLength);
                // Pick a random speed for the zombie.
                _currentMovementSpeed = Random.Range(minMovementSpeed, maxMovementSpeed);
            }
            // Check that last frame's X position has changed.
            if (_positionLastFrame.x != zombie.position.x)
            {
                // Get the zombie's scale.
                Vector2 zombieScale = zombie.localScale;
                // Check if we were farther left or right last frame.
                if (_positionLastFrame.x < zombie.position.x)
                {
                    // Set the direction the zombie is facing (by scale)
                    zombieScale.x = 1;
                }
                else
                {
                    // Set the direction the zombie is facing (by scale)
                    zombieScale.x = -1;
                }
                // Reassign the scale
                zombie.localScale = zombieScale;
            }
            // Save the zombie's position
            _positionLastFrame = zombie.position;
            // Wait one frame
            yield return 0;
        }
    }
}
// ---------------------- END OF FILE -----------------------
