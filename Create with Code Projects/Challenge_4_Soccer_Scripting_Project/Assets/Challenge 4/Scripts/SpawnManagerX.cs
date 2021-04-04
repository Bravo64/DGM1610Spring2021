using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class SpawnManagerX : MonoBehaviour
{

    private List<EnemyX> enemyPool = new List<EnemyX>();
    private float spawnRangeX = 10;
    private float spawnZMin = 15; // set min spawn Z
    private float spawnZMax = 25; // set max spawn Z

    private Rigidbody playerRigidbody;

    public int enemyCount;
    public int waveCount = 0;
    
    public GameObject powerupObject;
    public GameObject player;

    private void Start()
    {
        playerRigidbody = player.GetComponent<Rigidbody>();
        foreach (Transform child in transform)
        {
            enemyPool.Add(child.GetComponent<EnemyX>());
        }
        CheckEnemyCount();
    }

    // Update is called once per frame
    public void CheckEnemyCount()
    {
        if (enemyCount <= 0)
        {
            SpawnEnemyWave(waveCount);
        }
    }

    // Generate random spawn position for powerups and enemy balls
    Vector3 GenerateSpawnPosition ()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
    }


    void SpawnEnemyWave(int enemiesToSpawn)
    {
        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15); // make powerups spawn at player end

        // If no powerups remain, spawn a powerup
        if (!powerupObject.activeSelf) // check that there are zero powerups
        {
            powerupObject.transform.position = GenerateSpawnPosition() + powerupSpawnOffset;
            powerupObject.SetActive(true);
        }
        
        int spawnedEnemies = 0;
        // Spawn number of enemy balls based on wave number
        foreach (var enemy in enemyPool)
        {
            if (!enemy.gameObject.activeSelf)
            {
                enemy.transform.position = GenerateSpawnPosition();
                enemy.speed += waveCount * 10;
                enemy.gameObject.SetActive(true);
                enemyCount++;
                spawnedEnemies++;
            }
            if (spawnedEnemies > waveCount)
            {
                break;
            }
        }
        waveCount++;
        ResetPlayerPosition(); // put player back at start

    }

    // Move player back to position in front of own goal
    void ResetPlayerPosition ()
    {
        player.transform.position = new Vector3(0, 1, -7);
        playerRigidbody.velocity = Vector3.zero;
        playerRigidbody.angularVelocity = Vector3.zero;

    }

}
