using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] 
    private float spawnInterval = 3.0f;
    [SerializeField]
    private Transform minXMarker;
    [SerializeField] 
    private Transform maxXMarker;
    [SerializeField] 
    private Transform minZMarker;
    [SerializeField] 
    private Transform maxZMarker;
    
    private List<Rigidbody> _objectPool = new List<Rigidbody>();
    private WaitForSeconds _waitForSecondsObj;
    private int _waveNumber = 1;

    void Start()
    {
        _waitForSecondsObj = new WaitForSeconds(spawnInterval);
        foreach (Transform child in transform)
        {
            _objectPool.Add(child.GetComponent<Rigidbody>());
        }

        SpawnEnemies();
    }
    
    void SpawnEnemies()
    {
    int enemiesSpawned = 0;
        foreach (Rigidbody enemy in _objectPool)
        {
            if (!enemy.gameObject.activeSelf)
            {
                enemy.transform.position = GenerateSpawnPoint();
                enemy.gameObject.SetActive(true);
                enemy.velocity = Vector3.zero;
                enemy.angularVelocity = Vector3.zero;
                enemiesSpawned++;
                if (enemiesSpawned >= _waveNumber)
                {
                    break;
                }
            }
        }
    }

    public void CheckEnemyCount()
    {
        int activeEnemies = 0;
        foreach (Rigidbody enemy in _objectPool)
        {
            if (enemy.gameObject.activeSelf)
            {
                activeEnemies++;
                if (activeEnemies > 1)
                {
                    return;
                }
            }
        }
        _waveNumber++;
        SpawnEnemies();
    }

    Vector3 GenerateSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;
        spawnPoint.Set(Random.Range(minXMarker.position.x, maxXMarker.position.x),
            0,Random.Range(minZMarker.position.z, maxZMarker.position.z));
        return spawnPoint;
    }
}