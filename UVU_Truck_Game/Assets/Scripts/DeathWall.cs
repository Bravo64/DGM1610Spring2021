using System;
using System.Collections;
using System.Collections.Generic;
using GameEvents;
using UnityEngine;

public class DeathWall : MonoBehaviour
{
    /*
    ---------------- Documentation ---------------------

    Script's Name: DeathWall.cs
    Author: Keali'i Transfield
    
    ----------------------------------------------------
     */
    
    [Header("----------- VALUE VARIABLES -----------", order = 0)] [Space(10, order = 1)]
    
    // Our speed as of this moment (will get larger over time)
    [SerializeField]
    private float currentSpeed = 0.0f;
    // If this value is set to 1, the "currentSpeed"
    // variable will increase 1 unit per second.
    [SerializeField]
    private float speedIncreaseFactor = 1.0f;

    [Header("----------- SCRIPTABLE OBJECTS -----------", order = 2)] [Space(10, order = 3)]
    
    // Scriptable Object with the player's location.
    [SerializeField] private FloatData playerYLocationObj;

    [Header("----------- EVENTS -----------", order = 4)] [Space(10, order = 5)]
    
    // Scriptable Object Event letting scene loader know to reset level.
    [SerializeField] private VoidEvent restartLevelEvent;
    
    //------- The Update Method -----------
    // This Method is called once per frame.
    // Here, it is mainly used to move used
    // to move the wall forward at an ever
    // increasing speed.
    //--------------------------------------
    void Update()
    {
        // We want to match the player on the Y axis.
        // Get my position.
        Vector3 myPosition = transform.position;
        // Change it.
        myPosition.y = playerYLocationObj.value;
        // Reassign it.
        transform.position = myPosition;
        // Move forward over time.
        transform.Translate(Vector3.right * (currentSpeed * Time.deltaTime));
        // Increase out speed over time
        currentSpeed += Time.deltaTime * speedIncreaseFactor;
    }
    
    //------- The OnTriggerEnter2D Method ----------
    // This Method is called once another object enters
    // and triggers this object's trigger collider.
    // In this case, this wall object checks that the
    // object is a vehicle, and act accordingly
    // (working to end the level).
    //---------------------------------------
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Vehicle"))
        {
            // If we touch the player, let the
            // "updater" know through this event
            // (will reset the scene).
            restartLevelEvent.Raise();
        }
    }
}
