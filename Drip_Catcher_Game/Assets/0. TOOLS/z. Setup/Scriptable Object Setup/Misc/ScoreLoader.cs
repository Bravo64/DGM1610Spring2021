using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Score Loader Obj", menuName = "Misc/Score Loader Obj")]
public class ScoreLoader : ScriptableObject
{
    public IntData scoreIntDataObj;
    public bool attachSceneIndexToKey;
    public string scoreSaveKey;
    
    private string _completeKey;
    
    public void SaveScore()
    {
        if (attachSceneIndexToKey)
        {
            _completeKey = scoreSaveKey + SceneManager.GetActiveScene().buildIndex.ToString();
        }
        else
        {
            _completeKey = scoreSaveKey;
        }
        PlayerPrefs.SetInt (_completeKey, scoreIntDataObj.value);
    }
    
    public void LoadScore()
    {
        if (attachSceneIndexToKey)
        {
            _completeKey = scoreSaveKey + SceneManager.GetActiveScene().buildIndex.ToString();
        }
        else
        {
            _completeKey = scoreSaveKey;
        }
        PlayerPrefs.SetInt (scoreSaveKey, scoreIntDataObj.value);
        scoreIntDataObj.value = PlayerPrefs.GetInt(_completeKey, 0);
    }
}
