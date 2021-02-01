using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    /*
    ---------------- Documentation ---------------------

    Script's Name: RandomColor.cs
    Author: Keali'i Transfield

    Script's Description: This script assigns a random color
        to the sprite that it is attached to. In our case, we
        are using it to give the front end of the truck a random
        color on start. The random color is selected from an 
        array of random colors that are set up in the editor.
        
    Script's Methods:
        - Start
        - Update

    --------------------- DOC END ----------------------
     */
    
    //----------------- Variables ---------------------
    
    // Array of random colors to choose from (Set up in editor)
    [SerializeField] private Color[] colorOptions;
    // The sprite renderer of this object
    private SpriteRenderer _myRenderer;
    
    //-------------------------------------------------
    
    //-------------- The Start Method -------------------
    // This Method is called before the first frame update
    // (or at the gameObject's creation/reactivation). In
    // this script, it deals with getting the sprite renderer
    // and assigning a random color to the sprite.
    //-----------------------------------------------------
    void Start()
    {
        // Grab my sprite renderer.
        _myRenderer = GetComponent<SpriteRenderer>();
        // Pick a random color from the array of colors,
        // and assign it to the sprite
        _myRenderer.color = colorOptions[Random.Range(0, colorOptions.Length)];
    }
}
// -------------------------- END OF FILE ---------------------------
