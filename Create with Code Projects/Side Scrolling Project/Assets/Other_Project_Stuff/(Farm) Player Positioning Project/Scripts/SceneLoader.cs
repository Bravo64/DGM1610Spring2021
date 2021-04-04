using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] 
    private float gameOverPauseLength = 2.0f;
    [SerializeField] 
    private GameObject gameOverTextObject;
    [SerializeField] 
    private GameObject[] sceneObjects;
    [SerializeField] 
    private IntData livesObj;
    
    public void CheckOnLives()
    {
        if (livesObj.value <= -1)
        {
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        foreach (GameObject item in sceneObjects)
        {
            item.SetActive(false);
        }
        gameOverTextObject.SetActive(true);
        yield return new WaitForSeconds(gameOverPauseLength);
        ReloadScene();
    }
    
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
