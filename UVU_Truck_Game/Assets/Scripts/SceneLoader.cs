using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void ActivateReset()
    {
        StartCoroutine(RestartLevel());
    }
    
    public void ActivateLoadNext()
    {
        StartCoroutine(LoadNextLevel());
    }

    //-------- The RestartLevel Coroutine -------------
    // This Coroutine deals with restarting the current scene
    //--------------------------------------------
    private IEnumerator RestartLevel()
    {
        // Slow down time
        Time.timeScale = 0.5f;
        // Wait a bit using a for loop
        // (We can't use WaitForSeconds
        // because timeScale is affecting it).
        for (int i = 0; i < 180; i++)
        {
            // Wait one frame.
            yield return 0;
        }
        // Reset the time scale
        Time.timeScale = 1.0f;
        // Restart the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //-------- The LoadNextLevel Coroutine -------------
    // This Coroutine comes directly after the LevelComplete
    // method is called in the Updater. It deals with loading
    // the next level.
    //--------------------------------------------
    private IEnumerator LoadNextLevel()
    {
        // Wait a few seconds to give the "level complete" text some time to leave
        yield return new WaitForSeconds(3.0f);
        // Load the next scene index in the game's build settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
