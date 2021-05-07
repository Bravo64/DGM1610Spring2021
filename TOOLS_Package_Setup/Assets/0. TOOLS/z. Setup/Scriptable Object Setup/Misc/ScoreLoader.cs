using UnityEngine;

[CreateAssetMenu(fileName = "Score Loader Obj", menuName = "Misc/Score Loader Obj")]
public class ScoreLoader : ScriptableObject
{
    public IntData scoreIntDataObj;
    public string scoreSaveKey;
    
    public void SaveScore()
    {
        PlayerPrefs.SetInt (scoreSaveKey, scoreIntDataObj.value);
    }
    
    public void LoadScore()
    {
        scoreIntDataObj.value = PlayerPrefs.GetInt(scoreSaveKey, 0);
    }
}
