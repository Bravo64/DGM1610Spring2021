using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow2D : MonoBehaviour
{
    
    /*
    ---------------- Documentation ---------------------

    Script's Name: SmoothFollow2D.cs
    Author: Keali'i Transfield

    Script's Description: This script makes use of Mathf.SmoothDamp
    in order to smoothly follow another object along the X and Y Axis 
    (2D objects do not need to worry about Z Axis) This is all done 
    within the "FixedUpdate" method in order to reduce movement gitter.
    Altering the "smoothTime" variable will change how slow of fast the
    object's following speed is. This script will most likely just be
    used in order to have he background smoothly follow the main camera.
        
    Script's Methods:
        - FixedUpdate

    --------------------- DOC END ----------------------
    */
    
    [Header("------------- SCENE OBJECTS -------------", order = 0)] [Space(10, order = 1)]
    
    // The object we want to follow on the X and Y Axis
    // (probably the main camera).
    [SerializeField]
    private Transform targetObject;
    
    [Header("----------- VALUE VARIABLES -----------", order = 0)] [Space(10, order = 1)]
    
    // The amount of time we take
    // to reach our target destination
    // (If it is not moving).
    [UnityEngine.Range(0.0f, 10.0f)]
    [SerializeField]
    float smoothTime = 0.3F;
    
    
    // We need these private velocity variables
    // in order to use the "Mathf.SmoothDamp()" option.
    private float _xVelocity;
    private float _yVelocity;

    //------- The FixedUpdate Method ------
    // This Method is called a fixed number
    // of times per second (e.g. 50 FPS).
    // It is generally used when applying
    // force to the physics engine (collision
    // calculations are finished), and is similar to
    // "Update." In this case, we smooth out the
    // objects movement within this method
    // in order to reduce gitter.
    //--------------------------------------
    void FixedUpdate()
    {
        // Save our position.
        Vector3 myPostion = transform.position;
        // Save the target's position (reduces reference redundancy).
        Vector3 targetPostion = targetObject.position;
        // Smoothly move towards the camera on the X axis.
        myPostion.x = Mathf.SmoothDamp(myPostion.x, targetPostion.x, ref _xVelocity, smoothTime);
        // Smoothly move towards the camera on the Y axis.
        myPostion.y = Mathf.SmoothDamp(myPostion.y, targetPostion.y, ref _yVelocity, smoothTime);
        // Reassign our position.
        transform.position = myPostion;
    }
}
