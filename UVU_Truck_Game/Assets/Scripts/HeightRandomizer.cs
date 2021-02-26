using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightRandomizer : MonoBehaviour
{
    /*
---------------- Documentation ---------------------

Script's Name: RandomColorPalette.cs
Author: Keali'i Transfield

Script's Description: Gives an object a random height 
based inputted parameters within inspector visible variables.
    
Script's Methods:
    - Start

--------------------- DOC END ----------------------
 */
    
    [Header("---------- VALUE VARIABLES ----------", order = 0)] [Space(10, order = 0)]
    [Space(10, order = 1)]
    
    // The minimum allowed Y Scale
    [UnityEngine.Range(0.0f, 5.0f)]
    [SerializeField]
    private float minimumYScale = 0.5f;
    
    // The maximum allowed Y Scale
    [UnityEngine.Range(0.0f, 5.0f)]
    [SerializeField]
    private float maximumYScale = 1.15f;
    
    //-------------- The Start Method -------------------
    // This Method is called before the first frame update
    // (or at the gameObject's creation/reactivation). It
    // is mainly used for variable setup.
    //-----------------------------------------------------
    void Start()
    {
        // Grab my scale.
        Vector3 myScale = transform.localScale;
        // Randomize my scale on the Y axis.
        myScale.y = Random.Range(minimumYScale, maximumYScale);
        // Reassign my scale.
        transform.localScale = myScale;
    }
}
