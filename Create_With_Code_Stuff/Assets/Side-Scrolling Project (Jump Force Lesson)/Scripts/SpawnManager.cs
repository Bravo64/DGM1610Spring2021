using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] 
    private float minSpawnWaitTime = 2.0f;
    [SerializeField] 
    private float maxSpawnWaitTime = 5.0f;

    private int _randomObstacle;
    private List<GameObject> _obstaclePool = new List<GameObject>();
    private List<WaitForSeconds> _waitForSecondsObjs = new List<WaitForSeconds>();

    private void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            _obstaclePool.Add(child.gameObject);
        }

        for (int i = 0; i < 5; i++)
        {
            WaitForSeconds waitForSecondsObj = new WaitForSeconds(Random.Range(minSpawnWaitTime, maxSpawnWaitTime));
            _waitForSecondsObjs.Add(waitForSecondsObj);
        }

        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            yield return _waitForSecondsObjs[Random.Range(0, _waitForSecondsObjs.Count)];
            for (int i = 0; i < 50; i++)
            {
                _randomObstacle = Random.Range(0, _obstaclePool.Count);

                if (!_obstaclePool[_randomObstacle].activeSelf)
                {
                    _obstaclePool[_randomObstacle].transform.position = transform.position;
                    _obstaclePool[_randomObstacle].SetActive(true);
                    break;
                }
            }
        }
    }
}
