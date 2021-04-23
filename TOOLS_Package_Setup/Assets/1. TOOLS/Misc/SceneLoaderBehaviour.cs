using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderBehaviour : MonoBehaviour
{
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void LoadLastScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    
    public void LoadMainScene()
    {
        SceneManager.LoadScene(0);
    }
    
    public void LoadParticularScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
