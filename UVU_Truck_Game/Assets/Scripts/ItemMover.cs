using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemMover : MonoBehaviour
{
    /*
    ---------------- Documentation ---------------------

    Script's Name: ItemMover.cs
    Author: Keali'i Transfield

    Script's Description: This script deals with moving the
        pickup item over time along a straight line to each 
        waypoint. This will mainly be used for the re-fuel 
        pickup item. The method it uses is "MoveTowards"
        with a set of waypoint children.
        
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
    private float movementSpeed = 5.0f;
    
    [Header("--------------- SIBLINGS ---------------", order = 2)]
    [Space(10, order = 3)]
    
    // The item we are moving.
    [SerializeField]
    private Transform pickupItem;
    
    // Design my own enumeration variable
    enum CycleType{ pingPong, loop}

    [Header("--------------- ENUMERATIONS ---------------", order = 4)] 
    [Space(10, order = 5)]
    
    // Define a version of the enum
    [SerializeField] 
    CycleType movementCycleType = CycleType.pingPong;

    //------------------------------------------------------------


    //---------------- Private Variables -------------------

    // A list of (active) empties representing
    // each position we want to go to.
    private List<Transform> _waypoints = new List<Transform>();

    // A list of empties representing
    // each position we want to go to.
    private int _activeWaypoint = 0;

    // The direction we are moving in the waypoints,
    // which is kept track of with either a 1 or -1.
    private int _movementDirection = 1;

    //------------------------------------------------------


    //-------------- The Start Method -------------------
    // This Method is called before the first frame update
    // (or at the gameObject's creation/reactivation). It
    // is mainly used for setup and collecting the items
    // we need.
    //----------------------------------------------------
    void Start()
    {

        // Get the active waypoint children
        foreach (Transform child2 in transform)
        {
            // Add it to the list if it is active
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
    }

    //------- The Update Method ------------
    // This Method is called once per frame.
    // For this script, it moves the item
    // towards a specific waypoint. Once it
    // gets close enough, it switches to the
    // next one.
    //--------------------------------------
    void Update()
    {
        // Move the item towards the active waypoint.
        pickupItem.position = Vector3.MoveTowards(pickupItem.position, _waypoints[_activeWaypoint].position,
            movementSpeed * Time.deltaTime);
        if (Vector3.Distance(pickupItem.position, _waypoints[_activeWaypoint].position) < 0.01f)
        {
            // If we reach the end.
            if (_activeWaypoint >= _waypoints.Count - 1)
            {
                // Make sure we didn't go past.
                _activeWaypoint = _waypoints.Count - 1;
                // Check what type of loop we are making (using our enum).
                if (movementCycleType == CycleType.pingPong)
                {
                    // Go backwards (ping pong).
                    _movementDirection = -1;
                }
                else if (movementCycleType == CycleType.loop)
                {
                    // Move towards the first waypoint
                    // (-1 will change to -1 + 1 = waypoint 0).
                    _activeWaypoint = -1;
                }
            }
            // Else if we reach the beginning.
            else if (_activeWaypoint <= 0 && movementCycleType == CycleType.pingPong)
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
    }
}
// ---------------------- END OF FILE -----------------------
