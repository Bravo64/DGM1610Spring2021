using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevealTextByCharacter : MonoBehaviour
{
    /*
    ---------------- Documentation ---------------------

    Script's Name: RevealTextByCharacter.cs
    Author: Keali'i Transfield

    Script's Description: This script is intended to be attached to a UI
        Text Object, for the purpose of revealing in game dialog to the player
        over time, one character at a time. The user may change the speed of the
        reveal in the editor. In addition, punctuation marks and returns (newlines)
        receive an additional wait time, which can also be set in the editor. Audio
        is played in conjunction with the reveal (Audio Source Component).
        
    REQUIREMENTS:
        - Text Component
        - Audio Source Component (with typing audio)
        
    Script's Methods:
        - Start
        - RevealText (coroutine)

    --------------------- DOC END ----------------------
     */
    
    // Requirement header notes
    [Header("REQUIREMENTS:", order = 0)]
    [Header("----------------------------------------", order = 1)]
    [Header("- Text Component", order = 2)]
    [Header("- Audio Source Component (typing sounds)", order = 3)]
    [Header("----------------------------------------", order = 4)]
    
    // -- Serialize Variables (private, show in editor) --
    
    [Header("The speed at which the characters are revealed to the player:", order = 5)]
    // Character reveal speed variable
    [SerializeField] private float revealSpeed = 40.0f;
    [Header("How much larger the punctuation wait time is:", order = 6)]
    // The increase in punctuation wait times
    [SerializeField] private float punctuationWaitMultiplier = 15.0f;
    
    // ----- Private Variables -----
    
    private Text _myText;
    private AudioSource _myAudio;
    private string _originalMessage;
    private char[] _characterArray;

    
    //-------------- The Start Method -------------------
    // This Method is called before the first frame update
    // (or at the gameObject's creation/reactivation). It
    // is mainly used for initiating our coroutine.
    //-----------------------------------------------------
    void Start()
    {
        // Grab my text and audio components.
        _myText = GetComponent<Text>();
        _myAudio = GetComponent<AudioSource>();

        if (!_myText)
        {
            // If text component was not found, print an error and end the script's execution.
            Debug.LogError("Error: Revealing Text object is missing a Text Component.");
            this.enabled = false;
        }
        else if (!_myAudio)
        {
            // If audio component was not found, print an error and end the script's execution.
            Debug.LogError("Error: Revealing Text object is missing an " +
                           "Audio Source Component (with typing sound).");
            this.enabled = false;
        }
        // Save the original text
        _originalMessage = _myText.text;
        // Clear our text
        _myText.text = "";
        // Create an array of characters (letters) out of or message.
        _characterArray = _originalMessage.ToCharArray();
        StartCoroutine(RevealText());
    }

    //----------- The RevealText Coroutine ----------------
    // This Coroutine breaks the original text message down
    // by character, and reveals it to the player over time.
    // It give extra pauses to punctuation marks and returns
    // (newlines), and plays audio in conjunction with
    // the reveal.
    //-----------------------------------------------------
    IEnumerator RevealText()
    {
        // Loop through all letters (characters)
        for (var i = 0; i < _characterArray.Length; i++)
        {
            // Add on and print the active character
            _myText.text = _myText.text + _characterArray[i];
            // Check for punctuation (That is not a quotation or apostrophe).
            if (Char.IsPunctuation(_characterArray[i]) && 
            _characterArray[i].ToString() != "\"" && 
            _characterArray[i].ToString() != "'")
            {
                // Stop the typing sound
                // (in preparation for the pause)
                _myAudio.Stop();
                // Punctuation characters wait longer than others,
                // which the "punctuationWaitMultiplier" takes care of.
                yield return new WaitForSeconds(punctuationWaitMultiplier / revealSpeed);
            }
            // If not punctuation.
            else
            {
                // If the typing audio is not already playing, play it.
                if (!_myAudio.isPlaying)
                {
                    _myAudio.Play();
                }
                // Wait 1 second, divided by the reveal speed.
                yield return new WaitForSeconds(1.0f / revealSpeed);
            }
            // End of for loop cycle. Loop back to the beginning.
        }
        // Everything's finished. Make sure we stopped the audio.
        _myAudio.Stop();
    }
    
}
