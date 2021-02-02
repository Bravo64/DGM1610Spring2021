using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    /*
    ---------------- Documentation ---------------------
    
    Script's Name: LevelManager.cs
    Author: Keali'i Transfield
    
    Script's Description: This script is one of the most important
    scripts in the scene. This scripts deals with loading new scenes,
    keeping track of the players current score, and other functions
    that we be useful for multiple objects to have access to. This
    script is made static public by default, so that any object in
    the scene can easily gain access to it.
        
    Script's Methods:
        - Awake
        - GetPlayerScore
        - AddCoin
    
    --------------------- DOC END ----------------------
     */
    
    //----------------- Static Variables -------------------
    
    // This is our script. Doing this as static
    // makes the script available to everyone.
    public static LevelManager _instance;
    
    //----- Serialized Variables (private, shows in Editor) -----
    
    // This is the value of a coin pickup item.
    [SerializeField] private int coinValue = 150;

    //----------------- Private Variables -------------------
    
    // This is how much each coin is worth.
    private int _playerScore = 0;
    
    //------------------------------------------------------

    //-------------- The Awake Method -------------------
    // This Method is called before the first frame update
    // (or at the gameObject's creation/reactivation) and
    // ALSO before the Start method. Here it is mainly used
    // to set this script as the main static version.
    //-----------------------------------------------------
    void Awake()
    {
        // This script becomes static under "_instance"
        // (Accessible to everyone in scene)
        _instance = this;
    }
    
    //------- The GetPlayerScore Method ----------
    // This Method gives you the score when called.
    // However, the caller cannot change the score.
    //--------------------------------------------
    
    public int GetPlayerScore()
    {
        // Give the score
        return _playerScore;
    }
    
    //----------- The AddCoin Method ---------------
    // This Method adds one coin to the score based on
    // what the coin value is set to in the variable.
    //--------------------------------------------
    
    public void AddCoin()
    {
        // Add one coin.
        _playerScore += coinValue;
    }
}
