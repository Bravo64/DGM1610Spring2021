using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMover : MonoBehaviour
{
        /*
    ---------------- Documentation ---------------------

    Script's Name: TruckControls.cs

    Script's Description: This script deals with moving the
        pickup item over time along a straight line. This will
        mainly be used for the re-fuel pickup item. The method
        it uses it scaling along the Y axis at an even speed.
        
    Script's Methods:
        - Start
        - Update

    --------------------- DOC END ----------------------
     */
       
    
    //----- Serialized Variables (private, shows in Editor) ------

    [Header("Speed that Item moves along the line:")]
    [SerializeField] private float movementSpeed = 0.5f;
    
    //------------------------------------------------------------
        
    
    //---------------- Private Variables -------------------

    // The item we are moving.
    private Transform _pickupItem;
    
    // An empty object showing where
    // we want the pickup item to be.
    private Transform _desiredPositionEmpty;
    
    // This variable will keep track of which direction we
    // are scaling with either a 1 or -1.
    private int _growthDirection = 1;

    //------------------------------------------------------
        
    
    //-------------- The Start Method -------------------
    // This Method is called before the first frame update
    // (or at the gameObject's creation/reactivation). It
    // is mainly used for setup and collecting the items
    // we need.
    //----------------------------------------------------
    void Start()
    {
        // Search through children.
        foreach (Transform child1 in transform)
        {
            // Look for the position empty's name
            if (child1.name == "Position")
            {
                _desiredPositionEmpty = child1;
            }
        }
        // Search through our parent's children (siblings)
        foreach (Transform child2 in transform.parent)
        {
            // Look for the pickup item's tag ("Item").
            if (child2.CompareTag("Item"))
            {
                _pickupItem = child2;
            }
        }
        // Double check that we got the item.
        if (!_pickupItem)
        {
            // If not, print and error message and disable this script.
            Debug.Log("Error: Item for 'mover' is missing. Please ensure " +
                      "that the mover has an item sibling with an 'Item' tag.");
            this.enabled = false;
        }
        // Double check that we got the position empty.
        if (!_desiredPositionEmpty)
        {
            // If not, print and error message and disable this script.
            Debug.Log("Error: Mover's 'Position' child is missing.");
            this.enabled = false;
        }
    }

    //------- The Update Method ------------
    // This Method is called once per frame.
    // For this script, it mainly just changes
    // the "mover's" scale on the Y axis.
    //--------------------------------------
    void Update()
    {
        // Move the item to the desired position.
        _pickupItem.position = _desiredPositionEmpty.position;
        // Get the scale
        Vector3 myScale = transform.localScale;
        // Alter our scale along the Y Axis
        myScale.y += _growthDirection * movementSpeed * Time.deltaTime;
        // If the scale gets too big or small, flip the direction.
        if (myScale.y >= 1 || myScale.y <= -1)
        {
            // Times -1 flips the value
            _growthDirection *= -1;
            // Pull the scale back if it gets too far
            myScale.y = (float)Math.Round(myScale.y);
        }
        // Reassign the scale.
        transform.localScale = myScale;
    }
}
// ---------------------- END OF FILE -----------------------
