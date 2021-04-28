using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Scene Loader Obj", menuName = "Misc/Scene Loader Obj")]
public class SceneLoader : ScriptableObject
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
