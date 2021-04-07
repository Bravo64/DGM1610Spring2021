using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerX : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartButton; 

    public List<GameObject> targetPrefabs;

    private int score;
    private float spawnRate = 3.0f;
    public bool isGameActive;
    public int totalGameTime = 60;
    
    private float spaceBetweenSquares = 2.5f; 
    private float minValueX = -3.75f; //  x value of the center of the left-most square
    private float minValueY = -3.75f; //  y value of the center of the bottom-most square

    private int _setDifficulty;
    private int _timer;
    private WaitForSeconds oneSecondWaitObj;
    
    // Start the game, remove title screen, reset score, and adjust spawnRate based on difficulty button clicked
    public void StartGame(int difficultyInput)
    {
        _setDifficulty = difficultyInput;
        spawnRate /= _setDifficulty;
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
        titleScreen.SetActive(false);
        _timer = totalGameTime;
        timerText.text = "Time: " + _timer.ToString();
        oneSecondWaitObj = new WaitForSeconds(1.0f);
        StartCoroutine(RunTimer());
    }

    IEnumerator RunTimer()
    {
        while (_timer > 0)
        {
            yield return oneSecondWaitObj;
            _timer -= 1;
            timerText.text = "Time: " + _timer.ToString();
        }

        if (_timer <= 0)
        {
            GameOver();
        }
    }

    // While game is active spawn a random target
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            int index = Random.Range(0, targetPrefabs.Count);

            if (isGameActive)
            {
                GameObject spawnedTarget = (GameObject)Instantiate(targetPrefabs[index], RandomSpawnPosition(), targetPrefabs[index].transform.rotation);
                spawnedTarget.GetComponent<TargetX>().timeOnScreen /= _setDifficulty;
            }
            yield return new WaitForSeconds(spawnRate);
        }
    }

    // Generate a random spawn position based on a random index from 0 to 3
    Vector3 RandomSpawnPosition()
    {
        float spawnPosX = minValueX + (RandomSquareIndex() * spaceBetweenSquares);
        float spawnPosY = minValueY + (RandomSquareIndex() * spaceBetweenSquares);

        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);
        return spawnPosition;

    }

    // Generates random square index from 0 to 3, which determines which square the target will appear in
    int RandomSquareIndex()
    {
        return Random.Range(0, 4);
    }

    // Update score with value from target clicked
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "score: " + score.ToString();
    }

    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    // Restart game by reloading the scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
