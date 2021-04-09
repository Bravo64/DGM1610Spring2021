using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float minWaitTime = 1.0f;
    [SerializeField]
    private float maxWaitTime = 3.0f;
    [SerializeField] 
    private Transform minXMarker;
    [SerializeField] 
    private Transform maxXMarker;
    [SerializeField] 
    private Transform maxZMarker;

    private Vector3 spawnPosition;
    private List<GameObject> _enemyPool = new List<GameObject>();
    private List<WaitForSeconds> _waitForSecondsObjs = new List<WaitForSeconds>();
    
    void Start()
    {
        foreach (Transform child in transform)
        {
            _enemyPool.Add(child.gameObject);
        }

        for (int i = 0; i < 5; i++)
        {
            _waitForSecondsObjs.Add(new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime)));
        }
        
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            foreach (var enemy in _enemyPool)
            {
                if (!enemy.activeSelf)
                {
                    spawnPosition.Set(Random.Range(minXMarker.position.x,maxXMarker.position.x), 0, maxZMarker.position.z + 5.0f);
                    enemy.transform.position = spawnPosition;
                    enemy.transform.rotation = Quaternion.identity;
                    enemy.SetActive(true);
                    break;
                }
            }
            yield return _waitForSecondsObjs[Random.Range(0, _waitForSecondsObjs.Count)];
        }
    }
}
