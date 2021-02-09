using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        - LoadNextLevel (Coroutine)
    
    REQUIREMENTS:
        - Level Text Canvas Child
        - Score Text grandchild (canvas child)
        - Level Text grandchild (outline and regular)
        - Level Complete Text grandchild (outline and regular)
    
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
        _levelTextCanvas = transform.Find("Level_Text_Canvas");

        // Double check that we got the Canvas.
        if (!_levelTextCanvas)
        {
            // If not, print and error message and disable this script.
            Debug.LogError("Error: Canvas 'Level_Text_Canvas' is missing (Location " +
                           "in scene: /Level Manager --> Level_Text_Canvas).");
            this.enabled = false;
        }
        
        // Grab the Score Text from the Canvas's children
        _scoreText = _levelTextCanvas.Find("Score_Text").GetComponent<Text>();
        // Get the Audio Source as well.
        _scoreSound = _scoreText.GetComponent<AudioSource>();
        // Set the level number on the Level Outline Text.
        _levelTextCanvas.Find("Level_Text_Outline")
            .GetComponent<TextMeshProUGUI>()
            .text = "LEVEL " + levelNumber.ToString();
        // And the regular Level Text.
        _levelTextCanvas.Find("Level_Text_Outline")
            .Find("Level_Text").GetComponent<Text>()
            .text = "LEVEL " + levelNumber.ToString();

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
            yield return 0;
        }
        // Stop the sound when done.
        _scoreSound.Stop();
    }
    
    
    //-------- The LevelComplete Method -------------
    // This Method is called by the finish flag Object
    // once the player triggers the finish line.
    // It then goes through the process of completing
    // the level, displaying the level complete text,
    // and calling for the loading of the next level.
    //--------------------------------------------
    public void LevelComplete()
    {
        // Get the level complete text object from the Canvas's children
        Transform levelCompleteText = _levelTextCanvas.Find("Level_Complete_Text_Outline");
        
        // Double check that we got the Level Complete text.
        if (!levelCompleteText)
        {
            // If not, print and error message and disable this script.
            Debug.LogError("Error: 'Level_Complete_Text_Outline' grandchild is missing (Location " +
                           "in scene: /Level Manager --> Level_Text_Canvas --> Level_Complete_Text_Outline).");
            this.enabled = false;
        }
        
        // Enable the Level Complete text
        levelCompleteText.gameObject.SetActive(true);
        
        // Begin the coroutine that will load the next scene
        StartCoroutine(LoadNextLevel());
    }
    
    //-------- The LoadNextLevel Coroutine -------------
    // This Coroutine come directly after the LevelComplete
    // method is called. It deals with loading the next
    //--------------------------------------------
    private IEnumerator LoadNextLevel()
    {
        // Wait a few seconds to give the text some time to leave
        yield return new WaitForSeconds(3.0f);
        // Load the next scene index in the game's build settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
// ---------------------- END OF FILE -----------------------