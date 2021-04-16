using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] 
    private IntData livesObj;
    
    public void CheckOnLives()
    {
        if (livesObj.value <= -1)
        {
            ReloadScene();
        }
    }
    
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
