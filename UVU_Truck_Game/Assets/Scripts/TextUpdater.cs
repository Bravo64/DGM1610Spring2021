﻿using System.Collections;
using System.Collections.Generic;
using GameEvents;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
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
        - Start (non-public)
        - GetlevelScore
        - AddToScore (with value input)
        - AnimateScoreAdding (Coroutine)
        - LevelComplete
        - LoadNextLevel (Coroutine)
        - UpdateCrateText
    
    --------------------- DOC END ----------------------
     */

    //----- Serialized and Public Variables (Visible in Inspector) -----

    [Header("---------- VALUE VARIABLES ----------", order = 0)] [Space(10, order = 0)]
    [Space(10, order = 1)]
    
    // The Level Number
    [UnityEngine.Range(1, 50)]
    [SerializeField]
    private int levelNumber = 1;
    
    [Header("----------- TEXT OBJECTS -----------", order = 2)] [Space(10, order = 2)]
    [Space(10, order = 3)]
    
    // The Text component that displays current
    // level text outline (at level start).
    [SerializeField]
    private TextMeshProUGUI levelTextOutline;
    
    // The Text component that displays
    // current level text (at level start).
    [SerializeField]
    private Text levelText;
    
    // The Text component that displays the score to the player.
    [SerializeField]
    private Text _scoreText;

    // The Game Object that displays
    // level complete text (at level end).
    [SerializeField]
    private GameObject levelCompleteText;
    
    // The Text Component that displays
    // how many cure crates are still in the cargo.
    [SerializeField]
    private Text cureCrateText;
    
    [Header("--------------- AUDIO ---------------", order = 4)] [Space(10, order = 4)]
    [Space(10, order = 5)]
    
    // The Audio Source for the Score text "adding" sound.
    [SerializeField]
    private AudioSource scoreSound;
    
    [Header("--------- SCRIPTABLE OBJECTS ---------", order = 6)] [Space(10, order = 6)]
    [Space(10, order = 7)]
    
    // The Scriptable Object that will contain
    // the player score for this level.
    [SerializeField]
    private IntData levelScore;
    
    // The Scriptable Object that will contain
    // the number of cure crates still in the cargo.
    [SerializeField]
    private IntData carriedCrates;
    
    // The Scriptable Object that will contain
    // the number of cure crates that have not
    // touched the ground and died.
    [SerializeField]
    private IntData livingCrates;

    //--------------------------------------------------------

    private void Awake()
    {
        carriedCrates.value = 0;
        livingCrates.value = 0;
    }
    
    //-------------- The Start Method -------------------
    // This Method is called before the first frame update
    // (or at the gameObject's creation/reactivation). It
    // is mainly used for level text setup.
    //-----------------------------------------------------
    private void Start()
    {
        // Set the level text outline
        // to the current level number
        levelTextOutline.text = "LEVEL " + levelNumber.ToString();
        // And the regular Level Text.
        levelText.text = "LEVEL " + levelNumber.ToString();
        // Make sure we've reset the
        // player score for this level.
        levelScore.value = 0;
    }


    //----------- The AddCoin Method ---------------
    // This Method adds an entered amount to the
    // score variable
    //--------------------------------------------
    
    public void AddToScore(int amountToAdd)
    {
        // Save the old score value for the AnimateScoreAdding coroutine
        int oldScore = levelScore.value;
        // Add one coin.
        levelScore.value += amountToAdd;
        // Save the current score to the Value Manager Scriptable Object
        levelScore.value = levelScore.value;
        // "Animate" the score cycling upward with this coroutine
        StartCoroutine(AnimateScoreAdding(oldScore, levelScore.value));
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
            if (!scoreSound.isPlaying)
            {
                scoreSound.Play();
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
        scoreSound.Stop();
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
        // Enable the Level Complete text
        levelCompleteText.SetActive(true);
        
        // Begin the coroutine that will load the next scene
        StartCoroutine(LoadNextLevel());
    }

    //-------- The LoadNextLevel Coroutine -------------
    // This Coroutine comes directly after the LevelComplete
    // method is called. It deals with loading the next level.
    //--------------------------------------------
    private IEnumerator LoadNextLevel()
    {
        // Wait a few seconds to give the "level complete" text some time to leave
        yield return new WaitForSeconds(3.0f);
        // Load the next scene index in the game's build settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    //-------- The UpdateCrateText Method -------------
    // This Method is called through a game event, sent
    // by one of the crates if they detect that they
    // have either left or re-entered the truck's
    // back cargo. The "carriedCrates" Scriptable Object
    // holds that new amount we need to display.
    //--------------------------------------------
    public void UpdateCrateText()
    {
        // Update the text with the current amount.
        // (which the "carriedCrates" Scriptable Object holds)
        cureCrateText.text = "CURES: " + carriedCrates.value;
    }
}

// ---------------------- END OF FILE -----------------------