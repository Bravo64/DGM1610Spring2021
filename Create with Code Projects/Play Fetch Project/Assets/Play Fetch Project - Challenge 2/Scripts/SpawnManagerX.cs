using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] ballsPool;

    private float spawnLimitXLeft = -22;
    private float spawnLimitXRight = -7;
    private float spawnPosY = 30;

    private Vector3 _spawnPos;
    private float _startDelay = 1.0f;
    private float _minSpawnInterval = 3.0f;
    private float _maxSpawnInterval = 5.0f;
    private int _randomChoice;
    private List<WaitForSeconds> waitForSecondsObjList = new List<WaitForSeconds>();
    

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            WaitForSeconds waitForSecondsObj = new WaitForSeconds(Random.Range(_minSpawnInterval, _maxSpawnInterval));
            waitForSecondsObjList.Add(waitForSecondsObj);
        }
        StartCoroutine(SpawnRandomBall());
    }

    // Spawn random ball at random x position at top of play area
    IEnumerator SpawnRandomBall ()
    {
        yield return new WaitForSeconds(_startDelay);
        
        while (true)
        {
            // Generate random ball index and random spawn position
            _spawnPos.Set(Random.Range(spawnLimitXLeft, spawnLimitXRight), spawnPosY, 0);

            
            // instantiate ball at random spawn location
            for (int i = 0; i < 50; i++)
            {
                _randomChoice = Random.Range(0, ballsPool.Length);
                if (!ballsPool[_randomChoice].activeSelf)
                {
                    ballsPool[_randomChoice].transform.position = _spawnPos;
                    ballsPool[_randomChoice].SetActive(true);
                    break;
                }
            }

            yield return waitForSecondsObjList[Random.Range(0, waitForSecondsObjList.Count)];
        }
    }

}
