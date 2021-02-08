using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        - Awake (non-public)
        - Start (non-public)
        - GetPlayerScore
        - AddToScore (with value input)
        - AnimateScoreAdding (Coroutine)
        - LevelComplete
    
    REQUIREMENTS:
        -NA
    
    --------------------- DOC END ----------------------
     */

    //----------------- Static Variables -------------------

    // This is our script. Doing this as static
    // makes the script available to everyone.
    public static LevelManager _instance;

    //----- Serialized Variables (private, shows in Editor) -----

    // The Level Number
    [Header("This number must match the one", order = 0)]
    [Header("on the scene name (e.g. 'Level_1'):", order = 1)]
    [SerializeField] private int levelNumber = 1;

    //----------------- Private Variables -------------------
    
    // This is how much each coin is worth.
    private int _playerScore = 0;
    
    // The Canvas that all level text is on.
    private Transform _levelTextCanvas;
    
    // The Text component that displays the score to the player.
    private Text _scoreText;
    
    // The Audio Source for the Score text "adding" sound.
    private AudioSource _scoreSound;
    
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
    
    
    //-------------- The Start Method -------------------
    // This Method is called before the first frame update
    // (or at the gameObject's creation/reactivation). It
    // is mainly used for level text Game Object setup.
    //-----------------------------------------------------
    void Start()
    {
        // Grab the Level Text Canvas from the children
        foreach (Transform child1 in transform)
        {
            // Check for the Canvas's name
            if (child1.name == "Level_Text_Canvas")
            {
                // Save it
                _levelTextCanvas = child1;
            }
        }
        
        // Double check that we got the Canvas.
        if (!_levelTextCanvas)
        {
            // If not, print and error message and disable this script.
            Debug.LogError("Error: Canvas 'Level_Text_Canvas' is missing (Location " +
                           "in scene: /Level Manager --> Level_Text_Canvas).");
            this.enabled = false;
        }
        
        // Grab the Score Text from the Canvas's children
        foreach (Transform child2 in _levelTextCanvas)
        {
            // Check for the Score Text's name
            if (child2.name == "Score_Text")
            {
                // Get the Score text component and Save it
                _scoreText = child2.GetComponent<Text>();
            }
            
            // Check for the Level Text (Outline) name
            if (child2.name == "Level_Text_Outline")
            {
                // Put the correct level number in the level text message (at beginning of level).
                child2.GetComponent<TextMeshProUGUI>().text = "LEVEL " + levelNumber.ToString();
                // Also get the white inner text (which is a child).
                foreach (Transform child3 in child2.transform)
                {
                    // Put the correct level number here as well.
                    child3.GetComponent<Text>().text = "LEVEL " + levelNumber.ToString();
                }
            }
        }
        
        // Double check that we got the Score text.
        if (!_scoreText)
        {
            // If not, print and error message and disable this script.
            Debug.LogError("Error: 'Score_Text' Text Component is missing " +
                           "(Location in scene: /Level Manager --> Level_Text_Canvas --> Score_Text).");
            this.enabled = false;
        }

        // Get the score texts "adding" sound Audio Source Component
        _scoreSound = _scoreText.GetComponent<AudioSource>();

        // Double check that we got the Score "adding" sound.
        if (!_scoreSound)
        {
            // If not, print and error message and disable this script.
            Debug.LogError("Error: 'Score_Text' is missing the 'adding' sound Audio Source Component");
            this.enabled = false;
        }
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
    // This Method adds an entered amount to the
    // score variable
    //--------------------------------------------
    
    public void AddToScore(int amountToAdd)
    {
        // Save the old score value for the AnimateScoreAdding coroutine
        int oldScore = _playerScore;
        // Add one coin.
        _playerScore += amountToAdd;
        // "Animate" the score cycling upward with this coroutine
        StartCoroutine(AnimateScoreAdding(oldScore, _playerScore));
    }
    
    
    //-------- The AnimateScoreAdding Coroutine ---------
    // This Coroutine updates the Score Text over time
    // so that the visible on-screen number cycles up
    // in a sort of adding score "animation."
    //-----------------------------------------------
    private IEnumerator AnimateScoreAdding(int visableScore, int newScore)
    {
        // Add upward (though a while loop) until
        // we finally reach the real score.
        while (visableScore < newScore)
        {
            // Play the "adding" sound if not already playing
            if (!_scoreSound.isPlaying)
            {
                _scoreSound.Play();
            }
            // Add one to the score on every frame
            // in order to "Animate" an upward cycle.
            visableScore += 1;
            // Display the score to the player
            _scoreText.text = visableScore.ToString();
            // Wait one frame and then come back
            yield return 1;
        }
        // Stop the sound when done.
        _scoreSound.Stop();
    }
    
    
    //-------- The LevelComplete Method -------------
    // This Method is called by the finish flag Object
    // once the player triggers the finish line.
    // It then goes through the process of completing
    // the level (e.g. loading the next scene).
    //--------------------------------------------
    
    public void LevelComplete()
    {
        Debug.Log("Level Complete!");
    }
}
// ---------------------- END OF FILE -----------------------