using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI livesText;
    [SerializeField] 
    private IntData score;
    [SerializeField] 
    private IntData lives;
    

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.value.ToString();
    }

    public void UpdateLivesText()
    {
        if (lives.value > -1)
        {
            livesText.text = "Lives: " + lives.value.ToString();
        }
        else
        {
            livesText.text = "Lives: 0";
        }
    }
}
